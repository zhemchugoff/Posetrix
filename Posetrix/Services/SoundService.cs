using Posetrix.Assets;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using System.IO;
using System.Media;

namespace Posetrix.Services;

public class SoundService : ISoundService
{
    public void PlaySound(string soundFile)
    {
        SoundPlayer soundPlayer = new();

        string soundPath = soundFile switch
        {
            "Classic Countdown" => EmbeddedResourceLocator.CountDownSound1,
            "Beep Countdown" => EmbeddedResourceLocator.CountDownSound2,
            "Three Two One Countdown" => EmbeddedResourceLocator.CountDownSound3,
            _ => EmbeddedResourceLocator.CountDownSound1
        };

        Stream resourceStream = ResourceHelper.GetEmbeddedResourceStream(soundPath);
        soundPlayer.Stream = resourceStream;
        soundPlayer.Play();
    }
}
