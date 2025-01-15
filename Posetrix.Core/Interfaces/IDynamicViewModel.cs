namespace Posetrix.Core.Interfaces;

public interface IDynamicViewModel
{
    string DisplayName { get; } // Name for combobox.
    int GetSeconds();
}
