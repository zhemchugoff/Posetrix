namespace Posetrix.Core.Services;

public static class ResourceLocator
{
    // Paths.
    private const string PlaceholderPath = "pack://application:,,,/Assets/Images/";
    private const string SoundPath = "pack://application:,,,/Assets/Sounds/";

    // Placeholders.
    public static string WrongPathOrImageData => $"{PlaceholderPath}undraw_fixing-bugs_13mt.png";
    public static string Nullmage => $"{PlaceholderPath}undraw_bug-fixing_sgk7.png";
    public static string CelebrationImage => $"{PlaceholderPath}undraw_progress-data_gvcq.png";
    public static string DefaultPlaceholder => $"{PlaceholderPath}undraw_coffee-time_98vi.png"; // For MultiValueConverter.

    // Sounds.
    public static string CountDownSound1 => $"{SoundPath}up-to-the-top-of-the-hour-beep.ogg";
    public static string CountDownSound2 => $"{SoundPath}short-beep-countdown.ogg";
    public static string CountDownSound3 => $"{SoundPath}jenninexus-countdown.ogg";
}