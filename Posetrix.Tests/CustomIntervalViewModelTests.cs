using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class CustomIntervalViewModelTests
{
    [Fact]
    public void DisplayName_ShouldNotBeEmpty()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
        // Act.
        string actualDisplayName = customIntervalViewModel.DisplayName;
        // Assert.
        Assert.NotEmpty(actualDisplayName);
    }

    [Fact]
    public void DisplayName_ShouldHaveConstValue()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
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
    public void GetSeconds_ShouldReturnSeconds_WhenGivenSeconds(int seconds)
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
        customIntervalViewModel.Seconds = seconds;
        // Act.
        int actualSeconds = customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(seconds, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNullValue()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
        customIntervalViewModel.Seconds = null;
        // Act.
        int actualSeconds = customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(0, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNegativeValue()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
        customIntervalViewModel.Seconds = -1;
        // Act.
        int actualSeconds = customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(0, actualSeconds);
    }

    [Fact]
    public void Seconds_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        var customIntervalViewModel = new CustomIntervalViewModel();
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
