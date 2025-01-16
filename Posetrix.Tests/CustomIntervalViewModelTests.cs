using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class CustomIntervalViewModelTests
{
    private readonly CustomIntervalViewModel _customIntervalViewModel;
    public CustomIntervalViewModelTests()
    {
        _customIntervalViewModel = new CustomIntervalViewModel();
    }

    [Fact]
    public void DisplayName_ShouldNotBeEmpty_ShouldHasConstValue()
    {
        // Arrange.
        string expectedDisplayName = "Custom interval (in seconds)";
        // Act.
        string actualDisplayName = _customIntervalViewModel.DisplayName;
        // Assert.
        Assert.NotEmpty(actualDisplayName);
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
        _customIntervalViewModel.Seconds = seconds;
        // Act.
        int actualSeconds = _customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(seconds, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNullValue()
    {
        // Arrange.
        _customIntervalViewModel.Seconds = null;
        // Act.
        int actualSeconds = _customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(0, actualSeconds);
    }

    [Fact]
    public void GetSeconds_ShouldReturnZero_WhenGivenNegativeValue()
    {
        // Arrange.
        _customIntervalViewModel.Seconds = -1;
        // Act.
        int actualSeconds = _customIntervalViewModel.GetSeconds();
        // Assert.
        Assert.Equal(0, actualSeconds);
    }

    [Fact]
    public void Seconds_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        var propertyChangedCount = 0;
        _customIntervalViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        _customIntervalViewModel.Seconds = 5;
        _customIntervalViewModel.Seconds = 10;
        _customIntervalViewModel.Seconds = 12;
        // Assert.
        Assert.Equal(3, propertyChangedCount);
    }
}
