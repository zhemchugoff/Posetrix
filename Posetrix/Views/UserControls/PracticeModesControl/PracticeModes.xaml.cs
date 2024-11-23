﻿using Posetrix.Core.ViewModels;
using System.Windows.Controls;

namespace Posetrix.Views.UserControls;

/// <summary>
/// Interaction logic for PracticeModes.xaml
/// </summary>
public partial class PracticeModes : UserControl
{
    public PracticeModes(PracticeModesViewModel practiceModesViewModel)
    {
        InitializeComponent();
        DataContext = practiceModesViewModel;
    }
}
