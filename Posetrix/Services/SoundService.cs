using NAudio.Wave;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using System.Windows;

namespace Posetrix.Services;

public class SoundService : ISoundService
{
    public async Task PlaySound(string soundFile)
    {
        string soundPath = soundFile switch
        {
            "Classic Countdown" => ResourceLocator.CountDownSound1,
            "Beep Countdown" => ResourceLocator.CountDownSound2,
            "Three Two One Countdown" => ResourceLocator.CountDownSound3,
            _ => "Off"
        };

        if (soundPath != "Off")
        {
            try
            {
                Uri resourceUri = new(soundPath);
                var resourceInfo = Application.GetResourceStream(resourceUri);

                if (resourceInfo?.Stream != null)
                {
                    using (var audioStream = resourceInfo?.Stream)
                    {
                        using (var vorbisReader = new NAudio.Vorbis.VorbisWaveReader(audioStream))
                        using (var waveOut = new WaveOutEvent())
                        {
                            waveOut.Init(vorbisReader);
                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                await Task.Delay(100);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }
    }
}