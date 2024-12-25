using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using System.Media;
using System.Windows;

namespace Posetrix.Services;

public class SoundService : ISoundService
{
    private readonly SoundPlayer _soundPlayer = new();
    public void PlaySound(string soundFile)
    {
        string soundPath = soundFile switch
        {
            "Classic Countdown" => ResourceLocator.CountDownSound1,
            "Beep Countdown" => ResourceLocator.CountDownSound2,
            "Three Two One Countdown" => ResourceLocator.CountDownSound3,
            _ => "Off"
        };

        try
        {
            if (soundPath != "Off")
            {

                Uri packURI = new Uri(soundPath);

                var resourceStream = Application.GetResourceStream(packURI).Stream;

                if (resourceStream != null)
                {
                    _soundPlayer.Stream = resourceStream;
                    _soundPlayer.Play();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing sound: {ex.Message}");
        }
    }

    public void StopPlayer()
    {
        _soundPlayer.Stop();
    }

}
