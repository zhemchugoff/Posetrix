using NSubstitute;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using Shouldly;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Posetrix.Tests;

public class SessionViewModelTests
{
    private readonly ISoundService _mockISoundService;
    private readonly IUserSettings _mockUserSettings;
    private readonly IImageResolutionService _mockImageResolutionService;
    private readonly ISharedCollectionService _mockSharedCollectionService;
    private readonly ISharedSessionParametersService _mockSharedSessionParametersService;

    private readonly ImageFolder _imageFolder1 = new("C:\\TestFolder", "TestFolder", ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg",]);
    private readonly ImageFolder _imageFolder2 = new("C:\\TestFolder", "TestFolder", ["C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg",]);

    public SessionViewModelTests()
    {
        _mockISoundService = Substitute.For<ISoundService>();
        _mockUserSettings = Substitute.For<IUserSettings>();
        _mockImageResolutionService = Substitute.For<IImageResolutionService>();
        _mockSharedCollectionService = Substitute.For<ISharedCollectionService>();
        _mockSharedSessionParametersService = Substitute.For<ISharedSessionParametersService>();
    }

    [Fact]
    public void ViewModelName_ShouldHaveValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        var expectedViewModelName = ViewModelNames.Session;
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualViewModelName = sessionViewModel.ViewModelName;
        // Assert.
        actualViewModelName.ShouldBe(expectedViewModelName);
    }

    [Fact]
    public void WindowTitle_ShouldNotBeEmpty_WhenViewModelIsInitialized()
    {
        // Act.
        string actualWindowTitle = SessionViewModel.WindowTitle;
        // Assert.
        actualWindowTitle.ShouldNotBeEmpty();
    }

    [Fact]
    public void WindowTitle_ShouldHaveConstValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        string expectedWindowTitle = "Drawing session";
        // Act.
        string actualWindowTitle = SessionViewModel.WindowTitle;
        // Assert.
        actualWindowTitle.ShouldBe(expectedWindowTitle);
    }

    [Fact]
    public void IsSessionActive_ShouldBeTrue_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.IsSessionActive.ShouldBe(true);
    }

    [Fact]
    public void ImageWidth_ShouldBeZero_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.ImageWidth.ShouldBe(0);
    }

    [Fact]
    public void ImageHeight_ShouldBeZero_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.ImageHeight.ShouldBe(0);
    }

    [Fact]
    public void CurrentImage_ShouldHaveValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.CurrentImage.ShouldNotBeNull();
    }

    [Fact]
    public void ImageResolution_ShouldGetValueFromSettings_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        var resolution = "High";
        _mockUserSettings.ImageResolution.Returns(resolution);
        _mockImageResolutionService.SetResoluton(resolution).Returns(1920);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.ImageResolution.ShouldBe(1920);
    }

    [Fact]
    public void Seconds_ShouldGetValueFromSharedSessionParameters_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns(30);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.Seconds.ShouldBe(30);
    }

    [Fact]
    public void Seconds_ShouldBeZero_WhenSharedSessionParametersSecondsIsNull()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns((int?)null);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.Seconds.ShouldBe(0);
    }

    [Fact]
    public void IsTimeVisible_ShouldBeFalse_WhenSecondsIsZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns(0);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.IsTimeVisible.ShouldBeFalse();
    }

    [Fact]
    public void FormattedTime_ShouldBeEmpty_WhenSecondsIsZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns(0);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.FormattedTime.ShouldBeEmpty();
    }

    [Fact]
    public void IsTimeVisible_ShouldBeTrue_WhenSecondsIsNotZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns(15);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.IsTimeVisible.ShouldBeTrue();
    }

    [Fact]
    public void FormattedTime_ShouldNotBeEmpty_WhenSecondsIsNotZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.Seconds.Returns(15);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.FormattedTime.ShouldBe("00:00:00");
    }
    [Fact]
    public void CurrentImageIndex_ShouldBeZero_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.CurrentImageIndex.ShouldBe(0);
    }

    [Fact]
    public void CurrentImage_ShouldBeEqualToFirstCollectionItem_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns([_imageFolder1, _imageFolder2]);
        _mockSharedSessionParametersService.IsShuffleEnabled.Returns(false);
        _mockSharedSessionParametersService.SessionImageCount.Returns(3);
        var isShuffleOn = _mockSharedSessionParametersService.IsShuffleEnabled;
        var imageCount = _mockSharedSessionParametersService.SessionImageCount;

        List<string> sessionCollection = [];
        ImageCollectionHelpers.PopulateAndConvertObservableColletionToList(sessionCollection, _mockSharedCollectionService.ImageFolders, isShuffleOn, imageCount);
        var expectedValue = sessionCollection[0];
        // Act.
        var sessionViewModel = new SessionViewModel(_mockISoundService, _mockUserSettings, _mockImageResolutionService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        sessionViewModel.CurrentImage.ShouldBe(expectedValue);
    }
}
