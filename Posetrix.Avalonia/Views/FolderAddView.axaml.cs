using System.IO;
using Avalonia.Controls;
using Posetrix.Core.Data;

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