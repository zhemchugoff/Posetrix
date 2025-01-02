namespace Posetrix.Core.Services;

public static class ResourceLocator
{
    // Paths.
    private const string PlaceholderPath = "pack://application:,,,/Assets/Images/";
    private const string SoundPath = "pack://application:,,,/Assets/Sounds/";

    // Placeholders.
    public static string ErrorImage => $"{PlaceholderPath}undraw_fixing_bugs_w7gi.png";
    public static string ErrorImage1 => $"{PlaceholderPath}undraw_snap-the-moment_96zx.png";
    public static string Congratulations => $"{PlaceholderPath}undraw_winners_re_wr1l.png";
    public static string CelebrationImage => $"{PlaceholderPath}undraw_happy_music_g6wc.png";
    public static string CelebrationImage1 => $"{PlaceholderPath}jason-leung-Xaanw0s0pMk-unsplash.jpg";
    public static string DefaultPlaceholder => $"{PlaceholderPath}undraw_coffee-time_98vi.png"; // For MultiValueConverter.

    // Sounds.
    public static string CountDownSound1 => $"{SoundPath}up-to-the-top-of-the-hour-beep.ogg";
    public static string CountDownSound2 => $"{SoundPath}short-beep-countdown.ogg";
    public static string CountDownSound3 => $"{SoundPath}jenninexus-countdown.ogg";
}