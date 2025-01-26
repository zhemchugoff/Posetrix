using NSubstitute;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;
using Shouldly;

namespace Posetrix.Tests;

public class CustomIntervalViewModelTests
{
    private readonly ISharedSessionParametersService _mockSessionParametersService;
    public CustomIntervalViewModelTests()
    {
        _mockSessionParametersService = Substitute.For<ISharedSessionParametersService>();
    }

    [Fact]
    public void ViewModelName_ShouldHaveValue_WhenViewModelInitialized()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        var expectedViewModelName = ViewModelNames.CustomInterval;
        // Act.
        var actualViewModelName = customIntervalViewModel.ViewModelName;
        // Assert.
        actualViewModelName.ShouldBe(expectedViewModelName);
    }

    [Fact]
    public void DisplayName_ShouldNotBeEmpty_WhenViewModelInitialized()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        // Act.
        string actualDisplayName = customIntervalViewModel.DisplayName;
        // Assert.
        actualDisplayName.ShouldNotBeEmpty();
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue_WhenViewModelInitialized()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        // Act.
        string actualDisplayName = customIntervalViewModel.DisplayName;
        // Assert.
        actualDisplayName.ShouldBe("Custom interval (in seconds)");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(14)]
    [InlineData(23)]
    public void GetSeconds_ShouldReturnDefaultValue0(int seconds)
    {
        // Arrange.
        _mockSessionParametersService.Seconds.Returns(0);
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService)
        {
            Seconds = seconds
        };
        // Act.
        int? actualSeconds = customIntervalViewModel.Seconds;
        // Assert.
        actualSeconds.ShouldBe(seconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNullValue()
    {
        // Arrange.
        _mockSessionParametersService.Seconds.Returns(0);
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService)
        {
            Seconds = null
        };
        // Act.
        int? actualSeconds = customIntervalViewModel.Seconds;
        // Assert.
        actualSeconds.ShouldBe(0);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNegativeValue()
    {
        // Arrange.
        _mockSessionParametersService.Seconds.Returns(0);
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService)
        {
            Seconds = -1
        };
        // Act.
        int? actualSeconds = customIntervalViewModel.Seconds;
        // Assert.
        actualSeconds.ShouldBe(0);
    }

    [Fact]
    public void Seconds_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        _mockSessionParametersService.Seconds.Returns(0);
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        var propertyChangedCount = 0;
        customIntervalViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        customIntervalViewModel.Seconds = 5;
        customIntervalViewModel.Seconds = 10;
        customIntervalViewModel.Seconds = 12;
        // Assert.
        propertyChangedCount.ShouldBe(3);
    }
}
