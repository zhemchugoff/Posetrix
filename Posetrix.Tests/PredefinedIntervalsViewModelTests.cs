using Posetrix.Core.Enums;
using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class PredefinedIntervalsViewModelTests
{
    private readonly PredefinedIntervalsViewModel _predefinedIntervalsViewModel;

    public PredefinedIntervalsViewModelTests()
    {
        _predefinedIntervalsViewModel = new();
    }

    [Fact]
    public void DisplayName_ShouldNotBeEmpty()
    {
        // Act.
        string actualDisplayName = _predefinedIntervalsViewModel.DisplayName;
        // Assert.
        Assert.NotEmpty(actualDisplayName);
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue()
    {
        // Arrange.
        string expectedDisplayName = "Predefined intervals";
        // Act.
        string actualDisplayName = _predefinedIntervalsViewModel.DisplayName;
        // Assert.
        Assert.Equal(expectedDisplayName, actualDisplayName);
    }

    [Fact]
    public void GetSeconds_ShouldReturn30_WhenGivenInterval1()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(30, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn45_WhenGivenInterval2()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval2;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(45, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn60_WhenGivenInterval3()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval3;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(60, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn120_WhenGivenInterval4()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval4;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(120, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn300_WhenGivenInterval5()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval5;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(300, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturn600_WhenGivenInterval6()
    {
        // Arrange.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval6;
        // Act.
        int actualSeconds = _predefinedIntervalsViewModel.GetSeconds();
        // Assert.
        Assert.Equal(600, actualSeconds);
    }

    [Fact]
    public void SelectedInterval_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        var propertyChangedCount = 0;
        _predefinedIntervalsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval3;
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval4;
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        _predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        // Assert.
        Assert.Equal(3, propertyChangedCount);
    }
}
