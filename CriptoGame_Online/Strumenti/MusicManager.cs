using NAudio.Wave;

namespace Warrior_and_Wealth.Strumenti
{
    internal class MusicManager
    {
        // === CANALE MUSICA ===
        private static AudioFileReader? audioFile;
        private static WaveOutEvent? outputDevice;
        private static bool loop;
        private static string? currentTrack;
        private static float globalVolume = 1.0f;
        private static List<string>? currentPlaylist;
        private static int currentTrackIndex = 0;
        private static readonly Random random = new();
        private static volatile bool isStopping = false;
        private static readonly object _lock = new();

        // === CANALE DIALOGO ===
        private static AudioFileReader? dialogFile;
        private static WaveOutEvent? dialogDevice;
        private static List<string>? dialogQueue;
        private static int dialogIndex = 0;
        private static volatile bool dialogStopping = false;
        private static readonly object _dialogLock = new();
        private static float duckVolume = 0.25f;

        // ── MUSICA ──────────────────────────────────────────────

        public static void Play(string file)
        {
            if (currentTrack == file && outputDevice?.PlaybackState == PlaybackState.Playing)
                return;
            StopMusic();
            try
            {
                if (!File.Exists(file)) { Console.WriteLine($"File non trovato: {file}"); return; }
                audioFile = new AudioFileReader(file) { Volume = globalVolume };
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += OnPlaybackStopped;
                loop = false;
                currentTrack = file;
                currentPlaylist = null;
                outputDevice.Play();
            }
            catch (Exception ex) { Console.WriteLine($"Errore Play: {ex.Message}"); }
        }

        public static void PlayLoop(string file)
        {
            if (currentTrack == file && outputDevice?.PlaybackState == PlaybackState.Playing)
                return;
            StopMusic();
            try
            {
                if (!File.Exists(file)) { Console.WriteLine($"File non trovato: {file}"); return; }
                audioFile = new AudioFileReader(file) { Volume = globalVolume };
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += OnPlaybackStopped;
                loop = true;
                currentTrack = file;
                currentPlaylist = null;
                outputDevice.Play();
            }
            catch (Exception ex) { Console.WriteLine($"Errore PlayLoop: {ex.Message}"); }
        }

        public static void PlaySequence(List<string> sequence)
        {
            if (sequence == null || sequence.Count == 0) return;
            Task.Run(() =>
            {
                var valid = sequence.Where(File.Exists).ToList();
                if (valid.Count == 0) return;
                currentPlaylist = valid;
                currentTrackIndex = 0;
                loop = false;
                PlayTrackFromPlaylist();
            });
        }

        public static void PlayPlaylist(List<string> playlist, bool shuffle = true)
        {
            if (playlist == null || playlist.Count == 0) return;
            Task.Run(() =>
            {
                var valid = playlist.Where(File.Exists).ToList();
                if (valid.Count == 0) return;
                currentPlaylist = shuffle ? valid.OrderBy(_ => random.Next()).ToList() : valid;
                currentTrackIndex = 0;
                loop = true;
                PlayTrackFromPlaylist();
            });
        }

        private static void PlayTrackFromPlaylist()
        {
            if (currentPlaylist == null || currentPlaylist.Count == 0) return;
            if (currentTrackIndex >= currentPlaylist.Count) currentTrackIndex = 0;

            var track = currentPlaylist[currentTrackIndex];

            lock (_lock)
            {
                StopMusic();
                if (!File.Exists(track)) { NextTrack(); return; }
                try
                {
                    float vol = dialogDevice?.PlaybackState == PlaybackState.Playing
                        ? duckVolume * globalVolume
                        : globalVolume;

                    audioFile = new AudioFileReader(track) { Volume = vol };
                    outputDevice = new WaveOutEvent();
                    outputDevice.Init(audioFile);
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                    currentTrack = track;
                    outputDevice.Play();
                }
                catch (Exception ex) { Console.WriteLine($"Errore playlist: {ex.Message}"); NextTrack(); }
            }
        }

        private static void NextTrack()
        {
            if (currentPlaylist == null || currentPlaylist.Count == 0) return;
            currentTrackIndex++;
            if (currentTrackIndex >= currentPlaylist.Count)
            {
                if (loop)
                {
                    currentTrackIndex = 0;
                    currentPlaylist = currentPlaylist.OrderBy(_ => random.Next()).ToList();
                    PlayTrackFromPlaylist();
                }
                else
                {
                    currentPlaylist = null;
                    currentTrackIndex = 0;
                }
                return;
            }
            PlayTrackFromPlaylist();
        }

        private static void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (isStopping) return;
            var localAudioFile = audioFile;
            var localOutputDevice = outputDevice;
            var localPlaylist = currentPlaylist;

            Task.Run(() =>
            {
                if (isStopping) return;
                if (localPlaylist != null && localPlaylist.Count > 0)
                {
                    NextTrack();
                    return;
                }
                if (!loop || localAudioFile == null || localOutputDevice == null) return;
                try
                {
                    localAudioFile.Position = 0;
                    localOutputDevice.Play();
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex) { Console.WriteLine($"Errore loop: {ex.Message}"); }
            });
        }

        // ── CANALE DIALOGO ───────────────────────────────────────

        public static void PlayDialog(List<string> files, float musicDuckTo = 0.25f)
        {
            if (files == null || files.Count == 0) return;
            duckVolume = musicDuckTo;

            Task.Run(() =>
            {
                var valid = files.Where(File.Exists).ToList();
                if (valid.Count == 0) return;

                lock (_dialogLock)
                {
                    StopDialog();
                    dialogQueue = valid;
                    dialogIndex = 0;
                }

                ApplyDuck(globalVolume * musicDuckTo);
                PlayNextDialog();
            });
        }

        private static void PlayNextDialog()
        {
            lock (_dialogLock)
            {
                if (dialogQueue == null || dialogIndex >= dialogQueue.Count)
                {
                    RestoreMusicVolume();
                    dialogQueue = null;
                    dialogIndex = 0;
                    return;
                }

                var file = dialogQueue[dialogIndex];
                dialogStopping = false;

                try
                {
                    dialogFile = new AudioFileReader(file) { Volume = 1.0f };
                    dialogDevice = new WaveOutEvent();
                    dialogDevice.Init(dialogFile);
                    dialogDevice.PlaybackStopped += OnDialogStopped;
                    dialogDevice.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore dialogo: {ex.Message}");
                    dialogIndex++;
                    PlayNextDialog();
                }
            }
        }

        private static void OnDialogStopped(object? sender, StoppedEventArgs e)
        {
            if (dialogStopping) return;
            dialogIndex++;
            Task.Run(PlayNextDialog);
        }

        public static void StopDialog()
        {
            lock (_dialogLock)
            {
                dialogStopping = true;
                if (dialogDevice != null)
                {
                    try { dialogDevice.PlaybackStopped -= OnDialogStopped; dialogDevice.Stop(); dialogDevice.Dispose(); }
                    catch { }
                    dialogDevice = null;
                }
                if (dialogFile != null)
                {
                    try { dialogFile.Dispose(); } catch { }
                    dialogFile = null;
                }
                dialogQueue = null;
                dialogIndex = 0;
            }
            RestoreMusicVolume();
        }

        private static void ApplyDuck(float target)
        {
            if (audioFile != null)
                audioFile.Volume = Math.Clamp(target, 0f, 1f);
        }

        private static void RestoreMusicVolume()
        {
            if (audioFile != null)
                audioFile.Volume = globalVolume;
        }

        // ── VOLUME ──────────────────────────────────────────────

        public static void SetVolume(float volume)
        {
            globalVolume = Math.Clamp(volume, 0f, 1f);
            if (audioFile != null && dialogDevice?.PlaybackState != PlaybackState.Playing)
                audioFile.Volume = globalVolume;
        }

        public static async Task DuckVolumeAsync(float targetVolume = 0.3f, int ms = 500)
        {
            if (audioFile == null) return;
            float start = audioFile.Volume;
            targetVolume = Math.Clamp(targetVolume, 0f, 1f);
            for (int i = 0; i <= 20; i++)
            {
                audioFile.Volume = start + (targetVolume - start) * (i / 20f);
                await Task.Delay(ms / 20);
            }
        }

        public static async Task RestoreVolumeAsync(int ms = 500)
        {
            if (audioFile == null) return;
            float start = audioFile.Volume;
            for (int i = 0; i <= 20; i++)
            {
                audioFile.Volume = start + (globalVolume - start) * (i / 20f);
                await Task.Delay(ms / 20);
            }
        }

        public static async Task FadeInAsync(int ms = 1000)
        {
            if (audioFile == null) return;
            audioFile.Volume = 0f;
            for (int i = 0; i <= 20; i++)
            {
                audioFile.Volume = globalVolume * (i / 20f);
                await Task.Delay(ms / 20);
            }
        }

        public static async Task FadeOutAsync(int ms = 1000)
        {
            if (audioFile == null) return;
            float start = audioFile.Volume;
            for (int i = 0; i < 20; i++)
            {
                audioFile.Volume = start * (1f - (i / 20f));
                await Task.Delay(ms / 20);
            }
            StopMusic();
        }

        // ── STOP ────────────────────────────────────────────────

        public static void StopMusic()
        {
            lock (_lock)
            {
                isStopping = true;
                if (outputDevice != null)
                {
                    try { outputDevice.PlaybackStopped -= OnPlaybackStopped; outputDevice.Stop(); outputDevice.Dispose(); }
                    catch { }
                    outputDevice = null;
                }
                if (audioFile != null) { try { audioFile.Dispose(); } catch { } audioFile = null; }
                loop = false;
                currentTrack = null;
                isStopping = false;
            }
        }

        public static void Stop()
        {
            StopMusic();
            StopDialog();
        }

        public static bool IsPlaying => outputDevice?.PlaybackState == PlaybackState.Playing;
        public static bool IsDialogPlaying => dialogDevice?.PlaybackState == PlaybackState.Playing;
    }

    // ────────────────────────────────────────────────────────────────────────────

    internal class SoundManager
    {
        private static readonly Dictionary<string, CachedSound> soundCache = new();
        private static readonly List<WaveOutEvent> activeOutputs = new();
        private static float globalVolume = 0.8f;
        private static readonly object lockObj = new();

        public static void PreloadSound(string file)
        {
            if (soundCache.ContainsKey(file)) return;
            try
            {
                if (!File.Exists(file)) { Console.WriteLine($"File SFX non trovato: {file}"); return; }
                soundCache[file] = new CachedSound(file);
            }
            catch (Exception ex) { Console.WriteLine($"Errore preload SFX {file}: {ex.Message}"); }
        }

        public static void PlaySound(string file, float volume = 1.0f)
        {
            Task.Run(() => PlaySoundInternal(file, volume));
        }

        private static void PlaySoundInternal(string file, float volume)
        {
            try
            {
                if (!soundCache.ContainsKey(file))
                    PreloadSound(file);

                if (!soundCache.ContainsKey(file)) return;

                var sound = soundCache[file];
                var output = new WaveOutEvent();
                var provider = new CachedSoundSampleProvider(sound);

                output.Init(provider);
                output.Volume = Math.Clamp(volume * globalVolume, 0f, 1f);

                lock (lockObj) { activeOutputs.Add(output); }

                output.PlaybackStopped += (s, e) =>
                {
                    lock (lockObj) { activeOutputs.Remove(output); }
                    output.Dispose();
                };

                output.Play();
            }
            catch (Exception ex) { Console.WriteLine($"Errore riproduzione SFX: {ex.Message}"); }
        }

        public static void SetVolume(float volume)
        {
            globalVolume = Math.Clamp(volume, 0f, 1f);
        }

        public static void StopAll()
        {
            lock (lockObj)
            {
                foreach (var output in activeOutputs.ToArray())
                {
                    try { output.Stop(); output.Dispose(); } catch { }
                }
                activeOutputs.Clear();
            }
        }

        public static void ClearCache()
        {
            soundCache.Clear();
        }
    }

    // ────────────────────────────────────────────────────────────────────────────

    internal class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        public CachedSound(string audioFileName)
        {
            using var audioFileReader = new AudioFileReader(audioFileName);
            WaveFormat = audioFileReader.WaveFormat;

            var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
            var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
            int samplesRead;

            while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                wholeFile.AddRange(readBuffer.Take(samplesRead));

            AudioData = wholeFile.ToArray();
        }
    }

    // ────────────────────────────────────────────────────────────────────────────

    internal class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private long position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            this.cachedSound = cachedSound;
        }

        public WaveFormat WaveFormat => cachedSound.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = cachedSound.AudioData.Length - position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
            position += samplesToCopy;
            return (int)samplesToCopy;
        }
    }

    // ────────────────────────────────────────────────────────────────────────────

    public static class GameAudio
    {
        private static string GetPath(string relativePath) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        // === PLAYLIST MUSICA ===

        public static readonly List<string> PLAYLIST_GIOCO = new()
        {
            GetPath("Assets/Sound/Music/sword_5.mp3"),
            GetPath("Assets/Sound/Music/sword_7.mp3"),
            GetPath("Assets/Sound/Music/sword_8.mp3"),
            GetPath("Assets/Sound/Music/shadows-of-souls-ancient-medieval-cinematic-357891.mp3")
        };
        public static readonly List<string> PLAYLIST_LOGIN = new()
        {
            GetPath("Assets/Sound/Music/sword_1.mp3")
        };
        public static readonly List<string> PLAYLIST_VILLAGE = new()
        {
            GetPath("Assets/Sound/Music/sword_11.mp3")
        };
        public static readonly List<string> PLAYLIST_PVP = new()
        {
            GetPath("Assets/Sound/Music/sword_3.mp3"),
            GetPath("Assets/Sound/Music/battle_2.mp3")
        };
        public static readonly List<string> PLAYLIST_BUILD = new()
        {
            GetPath("Assets/Sound/Music/build_1.mp3")
        };

        // === PLAYLIST TUTORIAL ===

        public static readonly List<string> PLAYLIST_Introduzione_1 = new() { GetPath("Assets/Sound/Tutorial/Introduzione_1.mp3") };
        public static readonly List<string> PLAYLIST_Introduzione_2 = new() { GetPath("Assets/Sound/Tutorial/Introduzione_2.mp3") };
        public static readonly List<string> PLAYLIST_Risorse_1 = new() { GetPath("Assets/Sound/Tutorial/Risorse_1.mp3") };
        public static readonly List<string> PLAYLIST_DiamantiViola = new()
        {
            GetPath("Assets/Sound/Tutorial/DiamantiViola_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/DiamantiViola_pt2.mp3"),
            GetPath("Assets/Sound/Tutorial/DiamantiViola_pt3.mp3")
        };
        public static readonly List<string> PLAYLIST_DiamantiBlu = new()
        {
            GetPath("Assets/Sound/Tutorial/DiamantiBlu_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/DiamantiBlu_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_TributiFeudo = new() { GetPath("Assets/Sound/Tutorial/TributiFeudi.mp3") };
        public static readonly List<string> PLAYLIST_Feudi = new() { GetPath("Assets/Sound/Tutorial/Feudi.mp3") };
        public static readonly List<string> PLAYLIST_AcquistaFeudo = new()
        {
            GetPath("Assets/Sound/Tutorial/AcquistaFeudo_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/AcquistaFeudo_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Costruzione_1 = new() { GetPath("Assets/Sound/Tutorial/Costruzione_1.mp3") };
        public static readonly List<string> PLAYLIST_CivileMilitare = new()
        {
            GetPath("Assets/Sound/Tutorial/Strutture_CiviliMilitari_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Strutture_CiviliMilitari_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Costruzione_2 = new()
        {
            GetPath("Assets/Sound/Tutorial/Costruzione_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Costruzione_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Costruisci_Fattoria = new()
        {
            GetPath("Assets/Sound/Tutorial/Costruisci_Fattoria_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Costruisci_Fattoria_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Scambia = new()
        {
            GetPath("Assets/Sound/Tutorial/Scambia_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Scambia_pt2.mp3"),
            GetPath("Assets/Sound/Tutorial/Scambia_pt3.mp3"),
            GetPath("Assets/Sound/Tutorial/Scambia_pt4.mp3")
        };
        public static readonly List<string> PLAYLIST_Velocizza = new()
        {
            GetPath("Assets/Sound/Tutorial/Velocizza_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Velocizza_pt2.mp3"),
            GetPath("Assets/Sound/Tutorial/Velocizza_pt3.mp3"),  // FIX: era duplicato pt2
            GetPath("Assets/Sound/Tutorial/Velocizza_pt4.mp3")
        };
        public static readonly List<string> PLAYLIST_Costruisci_Segheria = new() { GetPath("Assets/Sound/Tutorial/Costruisci_Segheria.mp3") };
        public static readonly List<string> PLAYLIST_Costruisci_Cava = new() { GetPath("Assets/Sound/Tutorial/Costruisci_Cava.mp3") };
        public static readonly List<string> PLAYLIST_Costruisci_MinieraFerro = new() { GetPath("Assets/Sound/Tutorial/Costruisci_MinieraFerro.mp3") };
        public static readonly List<string> PLAYLIST_Costruisci_MinieraOro = new() { GetPath("Assets/Sound/Tutorial/Costruisci_MinieraOro.mp3") };
        public static readonly List<string> PLAYLIST_Costruisci_Casa = new() { GetPath("Assets/Sound/Tutorial/Costruisci_Casa.mp3") };
        public static readonly List<string> PLAYLIST_Strutture_Militari = new()
        {
            GetPath("Assets/Sound/Tutorial/Strutture_Militari_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Strutture_Militari_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Unita_Militari = new()
        {
            GetPath("Assets/Sound/Tutorial/Unita_Militari_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Unita_Militari_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Caserme = new() { GetPath("Assets/Sound/Tutorial/Caserme.mp3") };
        public static readonly List<string> PLAYLIST_Addestramento = new() { GetPath("Assets/Sound/Tutorial/Addestramento.mp3") };
        public static readonly List<string> PLAYLIST_Citta = new()
        {
            GetPath("Assets/Sound/Tutorial/Citta_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Citta_pt2.mp3"),
            GetPath("Assets/Sound/Tutorial/Citta_pt3.mp3"),
            GetPath("Assets/Sound/Tutorial/Citta_pt4.mp3")
        };
        public static readonly List<string> PLAYLIST_Riparazione = new()
        {
            GetPath("Assets/Sound/Tutorial/Riparazioni_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Riparazioni_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Guarnigione = new() { GetPath("Assets/Sound/Tutorial/Guarnigione.mp3") };
        public static readonly List<string> PLAYLIST_Statistiche = new() { GetPath("Assets/Sound/Tutorial/Statistiche.mp3") };
        public static readonly List<string> PLAYLIST_Shop = new() { GetPath("Assets/Sound/Tutorial/Shop.mp3") };
        public static readonly List<string> PLAYLIST_Ricerca = new()
        {
            GetPath("Assets/Sound/Tutorial/Ricerca_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Ricerca_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Quest_Mensili = new()
        {
            GetPath("Assets/Sound/Tutorial/Quest_Mensili_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Quest_Mensili_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Battaglia = new()
        {
            GetPath("Assets/Sound/Tutorial/Battaglie_pt1.mp3"),
            GetPath("Assets/Sound/Tutorial/Battaglie_pt2.mp3")
        };
        public static readonly List<string> PLAYLIST_Finale = new() { GetPath("Assets/Sound/Tutorial/Finale.mp3") };

        // ── PLAY ────────────────────────────────────────────────

        public static void PlayMenuMusic(string musica)
        {
            switch (musica)
            {
                // Musica di sottofondo (loop/shuffle)
                case "Gioco": MusicManager.PlayPlaylist(PLAYLIST_GIOCO, shuffle: true); break;
                case "Login": MusicManager.PlayPlaylist(PLAYLIST_LOGIN, shuffle: true); break;
                case "Villaggio": MusicManager.PlayPlaylist(PLAYLIST_VILLAGE, shuffle: true); break;
                case "PVP": MusicManager.PlayPlaylist(PLAYLIST_PVP, shuffle: true); break;
                case "Build": MusicManager.PlayPlaylist(PLAYLIST_BUILD, shuffle: true); break;

                // Dialoghi tutorial (canale separato con duck musica)
                case "Tutorial - 1": MusicManager.PlayDialog(PLAYLIST_Introduzione_1, musicDuckTo: 0.2f); break;
                case "Tutorial - 2": MusicManager.PlayDialog(PLAYLIST_Introduzione_2, musicDuckTo: 0.2f); break;
                case "Tutorial - 3": MusicManager.PlayDialog(PLAYLIST_Risorse_1, musicDuckTo: 0.2f); break;
                case "Tutorial - 4": MusicManager.PlayDialog(PLAYLIST_DiamantiViola, musicDuckTo: 0.2f); break;
                case "Tutorial - 5": MusicManager.PlayDialog(PLAYLIST_DiamantiBlu, musicDuckTo: 0.2f); break;
                case "Tutorial - 6": MusicManager.PlayDialog(PLAYLIST_TributiFeudo, musicDuckTo: 0.2f); break;
                case "Tutorial - 7": MusicManager.PlayDialog(PLAYLIST_Feudi, musicDuckTo: 0.2f); break;
                case "Tutorial - 8": MusicManager.PlayDialog(PLAYLIST_AcquistaFeudo, musicDuckTo: 0.2f); break;
                case "Tutorial - 9": MusicManager.PlayDialog(PLAYLIST_Costruzione_1, musicDuckTo: 0.2f); break;
                case "Tutorial - 10": MusicManager.PlayDialog(PLAYLIST_CivileMilitare, musicDuckTo: 0.2f); break;
                case "Tutorial - 11": MusicManager.PlayDialog(PLAYLIST_Costruzione_2, musicDuckTo: 0.2f); break;
                case "Tutorial - 12": MusicManager.PlayDialog(PLAYLIST_Costruisci_Fattoria, musicDuckTo: 0.2f); break;
                case "Tutorial - 13": MusicManager.PlayDialog(PLAYLIST_Scambia, musicDuckTo: 0.2f); break;
                case "Tutorial - 14": MusicManager.PlayDialog(PLAYLIST_Velocizza, musicDuckTo: 0.2f); break;
                case "Tutorial - 15": MusicManager.PlayDialog(PLAYLIST_Costruisci_Segheria, musicDuckTo: 0.2f); break;
                case "Tutorial - 16": MusicManager.PlayDialog(PLAYLIST_Costruisci_Cava, musicDuckTo: 0.2f); break;
                case "Tutorial - 17": MusicManager.PlayDialog(PLAYLIST_Costruisci_MinieraFerro, musicDuckTo: 0.2f); break;
                case "Tutorial - 18": MusicManager.PlayDialog(PLAYLIST_Costruisci_MinieraOro, musicDuckTo: 0.2f); break;
                case "Tutorial - 19": MusicManager.PlayDialog(PLAYLIST_Costruisci_Casa, musicDuckTo: 0.2f); break;
                case "Tutorial - 20": MusicManager.PlayDialog(PLAYLIST_Strutture_Militari, musicDuckTo: 0.2f); break;
                case "Tutorial - 21": MusicManager.PlayDialog(PLAYLIST_Unita_Militari, musicDuckTo: 0.2f); break;
                case "Tutorial - 22": MusicManager.PlayDialog(PLAYLIST_Caserme, musicDuckTo: 0.2f); break;
                case "Tutorial - 23": MusicManager.PlayDialog(PLAYLIST_Addestramento, musicDuckTo: 0.2f); break;
                case "Tutorial - 24": MusicManager.PlayDialog(PLAYLIST_Citta, musicDuckTo: 0.2f); break;
                case "Tutorial - 25": MusicManager.PlayDialog(PLAYLIST_Riparazione, musicDuckTo: 0.2f); break;
                case "Tutorial - 26": MusicManager.PlayDialog(PLAYLIST_Guarnigione, musicDuckTo: 0.2f); break;
                case "Tutorial - 27": MusicManager.PlayDialog(PLAYLIST_Statistiche, musicDuckTo: 0.2f); break;
                case "Tutorial - 28": MusicManager.PlayDialog(PLAYLIST_Shop, musicDuckTo: 0.2f); break;
                case "Tutorial - 29": MusicManager.PlayDialog(PLAYLIST_Ricerca, musicDuckTo: 0.2f); break;
                case "Tutorial - 30": MusicManager.PlayDialog(PLAYLIST_Quest_Mensili, musicDuckTo: 0.2f); break;
                case "Tutorial - 31": MusicManager.PlayDialog(PLAYLIST_Battaglia, musicDuckTo: 0.2f); break;
                case "Tutorial - 32": MusicManager.PlayDialog(PLAYLIST_Finale, musicDuckTo: 0.2f); break;
            }
        }

        public static void StopMusic() => MusicManager.StopMusic();
        public static void StopDialog() => MusicManager.StopDialog();

        public static void Cleanup()
        {
            MusicManager.Stop();
            SoundManager.StopAll();
            SoundManager.ClearCache();
        }
    }
}