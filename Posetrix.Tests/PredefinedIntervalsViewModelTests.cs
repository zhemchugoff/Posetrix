using Posetrix.Core.Enums;
using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class PredefinedIntervalsViewModelTests
{
    [Fact]
    public void DisplayName_ShouldNotBeEmpty()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        // Act.
        string actualDisplayName = predefinedIntervalsViewModel.DisplayName;
        // Assert.
        Assert.NotEmpty(actualDisplayName);
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        string expectedDisplayName = "Predefined intervals";
        // Act.
        string actualDisplayName = predefinedIntervalsViewModel.DisplayName;
        // Assert.
        Assert.Equal(expectedDisplayName, actualDisplayName);
    }

    [Fact]
    public void GetSeconds_ShouldReturn30_WhenGivenInterval1()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(30, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn45_WhenGivenInterval2()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval2;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(45, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn60_WhenGivenInterval3()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval3;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(60, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn120_WhenGivenInterval4()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval4;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(120, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn300_WhenGivenInterval5()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval5;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(300, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn600_WhenGivenInterval6()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval6;
        // Act.
        int actualSeconds = predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(600, actualSeconds);
    }

    [Fact]
    public void SelectedInterval_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel();
        var propertyChangedCount = 0;
        predefinedIntervalsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval3;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval4;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        // Assert.
        Assert.Equal(3, propertyChangedCount);
    }
}
