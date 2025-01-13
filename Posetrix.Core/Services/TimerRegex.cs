using System.Text.RegularExpressions;

namespace Posetrix.Core.Services;

/// <summary>
/// A helper class for timer textbox input.
/// </summary>
public partial class TimerRegex
{
    // Allow only numeric input.
    [GeneratedRegex(@"^[0-9]+$", RegexOptions.None)]
    public static partial Regex TimerValuesRegex();
}
