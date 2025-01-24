using NSubstitute;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;
using Shouldly;

namespace Posetrix.Tests;

public class PredefinedIntervalsViewModelTests
{
    private readonly ISharedSessionParametersService _mockSessionParametersService;

    public PredefinedIntervalsViewModelTests()
    {
        _mockSessionParametersService = Substitute.For<ISharedSessionParametersService>();
    }
    [Fact]
    public void DisplayName_ShouldNotBeEmpty()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService);
        // Act.
        string actualDisplayName = predefinedIntervalsViewModel.DisplayName;
        // Assert.
        actualDisplayName.ShouldNotBeEmpty();
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService);
        string expectedDisplayName = "Predefined intervals";
        // Act.
        string actualDisplayName = predefinedIntervalsViewModel.DisplayName;
        // Assert.
        actualDisplayName.ShouldBe(expectedDisplayName);
    }

    [Fact]
    public void ConvertEnumToSeconds_ShouldReturn30_WhenGivenInterval1()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval1
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(30);
    }

    [Fact]
    public void GetSeconds_ShouldReturn45_WhenGivenInterval2()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval2
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(45);
    }

    [Fact]
    public void GetSeconds_ShouldReturn60_WhenGivenInterval3()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval3
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(60);
    }

    [Fact]
    public void GetSeconds_ShouldReturn120_WhenGivenInterval4()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval4
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(120);
    }

    [Fact]
    public void GetSeconds_ShouldReturn300_WhenGivenInterval5()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval5
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(300);
    }

    [Fact]
    public void GetSeconds_ShouldReturn600_WhenGivenInterval6()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService)
        {
            SelectedInterval = Intervals.Interval6
        };
        // Act.
        int actualSeconds = PredefinedIntervalsViewModel.ConvertEnumToSeconds(predefinedIntervalsViewModel.SelectedInterval);
        // Assert.
        actualSeconds.ShouldBe(600);
    }

    [Fact]
    public void SelectedInterval_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        var predefinedIntervalsViewModel = new PredefinedIntervalsViewModel(_mockSessionParametersService);
        var propertyChangedCount = 0;
        predefinedIntervalsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval3;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval4;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        predefinedIntervalsViewModel.SelectedInterval = Intervals.Interval1;
        // Assert.
        propertyChangedCount.ShouldBe(3);
    }
}
