using System;
using System.IO;
using Posetrix.Core.Interfaces;
using LibVLCSharp.Shared;
using Posetrix.Assets;
using Posetrix.Core.Services;

namespace Posetrix.Avalonia.Services;

public class SoundService : ISoundService
{
    private LibVLC _libVLC;

    private MediaPlayer _mediaPlayer;

    public void PlaySound(string soundFile)
    {
        _libVLC = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVLC);
        
        string soundPath = soundFile switch
        {
            "Classic Countdown" => EmbeddedResourceLocator.CountDownSound1,
            "Beep Countdown" => EmbeddedResourceLocator.CountDownSound2,
            "Three Two One Countdown" => EmbeddedResourceLocator.CountDownSound3,
            _ => "Off"
        };

        if (soundPath != "Off")
        {
            Stream resourceStream = ResourceHelper.GetEmbeddedResourceStream(soundPath);

// Create Media from Stream
            Media media = new Media(_libVLC, new Uri("dummy://")); 
            // media.SetDataSource(resourceStream); 
            _mediaPlayer.Play(media);
            // soundPlayer.Stream = resourceStream;
            // soundPlayer.Play();
        }
    }
}