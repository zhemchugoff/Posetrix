using NAudio.Wave;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Media;

namespace Posetrix.Services;

public class SoundService : ISoundService
{
    private WaveOutEvent? _outputDevice;
    private Stream? _audioStream;
    public void PlaySound(string soundFile)
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
                // Dispose any existing playback to avoid conflicts.
                StopPlayback();

                // Initialize playback.
                Uri resourceUri = new Uri(soundPath);
                _audioStream = Application.GetResourceStream(resourceUri).Stream;

                if (_audioStream != null)
                {
                    var vorbisReader = new NAudio.Vorbis.VorbisWaveReader(_audioStream);

                    _outputDevice = new WaveOutEvent();
                    _outputDevice.Init(vorbisReader);
                    _outputDevice.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }
    }

    public void StopPlayback()
    {
        _outputDevice?.Stop();
        _audioStream?.Dispose();
        _outputDevice?.Dispose();
    }
}
