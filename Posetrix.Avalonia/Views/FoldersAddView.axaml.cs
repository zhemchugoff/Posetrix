using Avalonia.Controls;
using Posetrix.Core.Services;
using System.IO;

namespace Posetrix.Avalonia.Views;

public partial class FoldersAddView : Window
{
    public FoldersAddView()
    {
        InitializeComponent();
        using Stream stream = Assets.ResourceHelper.GetEmbeddedResourceStream(EmbeddedResourceLocator.WindowIcon);
        Icon = new WindowIcon(stream);
    }
}