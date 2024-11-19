using Microsoft.Win32;
using Posetrix.Core.ViewModels;
using System.Windows.Controls;

namespace Posetrix.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PracticeModes.xaml
    /// </summary>
    public partial class PracticeModes : UserControl
    {
        public PracticeModes()
        {
            InitializeComponent();
            DataContext = new PracticeModesViewModel();
        }
    }
}
