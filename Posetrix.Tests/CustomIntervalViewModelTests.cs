using NSubstitute;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class CustomIntervalViewModelTests
{
    private readonly ISharedSessionParametersService _mockSessionParametersService;
    public CustomIntervalViewModelTests()
    {
        _mockSessionParametersService = Substitute.For<ISharedSessionParametersService>();
    }
    [Fact]
    public void DisplayName_ShouldNotBeEmpty()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        // Act.
        string actualDisplayName = customIntervalViewModel.DisplayName;
        // Assert.
        Assert.NotEmpty(actualDisplayName);
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel(_mockSessionParametersService);
        string expectedDisplayName = "Custom interval (in seconds)";
        // Act.
        string actualDisplayName = customIntervalViewModel.DisplayName;
        // Assert.
        Assert.Equal(expectedDisplayName, actualDisplayName);
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
        Assert.Equal(seconds, actualSeconds);
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
        Assert.Equal(0, actualSeconds);
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
        Assert.Equal(0, actualSeconds);
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
        Assert.Equal(3, propertyChangedCount);
    }
}
