using Avalonia.Controls;
using Posetrix.Core.Services;
using System.IO;

namespace Posetrix.Avalonia.Views;

public partial class FolderAddView : Window
{
    public FolderAddView()
    {
        InitializeComponent();
        using Stream stream = Assets.ResourceHelper.GetEmbeddedResourceStream(PlaceHolderService.WindowIcon);
        Icon = new WindowIcon(stream);
    }
}