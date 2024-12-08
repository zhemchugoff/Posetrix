namespace Posetrix.Core.Services;

public static class PlaceHolderService
{
    private const string PathBase  = "pack://application:,,,/Posetrix.SharedAssets;component";
    private const string AssetsPath  = "pack://application:,,,/Posetrix.Assets;component";
    private const string IconPath = "Icons.";
    private const string ImagePath = "Images.";

    public static string GetSessionEndImage => $"{PathBase}/Images/undraw_winners_re.png";
    public static string GetErrorImage => $"{PathBase}/Images/undraw_fixing_bugs.png";
    public static string GetIncorrectPathImage => $"{PathBase}/Images/undraw_bug_fixing.png";
    public static string WPFWindowIcon => $"{AssetsPath}/Icons/pencil-light.png";

    public static string ErrorImage => $"{ImagePath}undraw_fixing_bugs_w7gi.png";
    public static string Congratulations => $"{ImagePath}undraw_winners_re_wr1l.png";
    public static string WindowIcon => $"{IconPath}pencil-light.png";
}