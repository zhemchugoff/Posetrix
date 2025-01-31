using NSubstitute;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.ViewModels;
using Shouldly;
using System.Collections.ObjectModel;

namespace Posetrix.Tests;

public class MainViewModelTests
{
    private readonly IWindowManager _mockWindowManager;
    private readonly IViewModelFactory _mockViewModelFactory;
    private readonly ISharedCollectionService _mockSharedCollectionService;
    private readonly ISharedSessionParametersService _mockSharedSessionParametersService;
    private readonly IUserSettings _mockUserSettings;
    private readonly IThemeService _mockThemeService;

    public MainViewModelTests()
    {
        _mockWindowManager = Substitute.For<IWindowManager>();
        _mockViewModelFactory = Substitute.For<IViewModelFactory>();
        _mockSharedCollectionService = Substitute.For<ISharedCollectionService>();
        _mockSharedSessionParametersService = Substitute.For<ISharedSessionParametersService>();
        _mockUserSettings = Substitute.For<IUserSettings>();
        _mockThemeService = Substitute.For<IThemeService>();
    }

    [Fact]
    public void ViewModelName_ShouldHaveValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        var expectedViewModelName = ViewModelNames.Main;
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualViewModelName = mainViewModel.ViewModelName;
        // Assert.
        actualViewModelName.ShouldBe(expectedViewModelName);
    }

    [Fact]
    public void WindowTitle_ShouldNotBeEmpty_WhenViewModelIsInitialized()
    {
        // Act.
        string actualWindowTitle = MainViewModel.WindowTitle;
        // Assert.
        actualWindowTitle.ShouldNotBeEmpty();
    }

    [Fact]
    public void WindowTitle_ShouldHaveConstValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        string expectedWindowTitle = "Posetrix";
        // Act.
        string actualWindowTitle = MainViewModel.WindowTitle;
        // Assert.
        actualWindowTitle.ShouldBe(expectedWindowTitle);
    }

    [Fact]
    public void ComboBoxItems_ShouldHaveValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        List<string> expectedList = ["Predefined intervals", "Custom interval (in seconds)"];
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualList = mainViewModel.ComboboxItems;
        // Assert.
        actualList.ShouldBeEquivalentTo(expectedList);
    }

    [Fact]
    public void SelectedComboboxItem_ShouldBeEqualToFirstListItem_WhenViewModelIsInitialized()
    {
        // Arrange.
        string expectedValue = "Predefined intervals";
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        mainViewModel.SelectedComboboxItem.ShouldBe(expectedValue);
    }

    [Fact]
    public void FolderCount_ShouldHaveDefaultValue0_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.FolderCount;
        // Assert.
        actualValue.ShouldBe(0);
    }

    [Fact]
    public void ImageCountInfo_ShouldHaveDefaultValue0_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.ImageCountInfo;
        // Assert.
        actualValue.ShouldBe(0);
    }

    [Fact]
    public void CanStartSession_ShouldBeFalse_WhenFolderCountIsZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        mainViewModel.FolderCount = 0;
        // Assert.
        mainViewModel.CanStartSession.ShouldBeFalse();
    }

    [Fact]
    public void CanStartSession_ShouldBeTrue_WhenFolderCountIsNotZero()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        mainViewModel.FolderCount = 4;
        // Assert.
        mainViewModel.CanStartSession.ShouldBeTrue();
    }

    [Fact]
    public void SessionImageCount_ShouldGetValueFromSharedSessionParametersService_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.SessionImageCount.Returns(0);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.SessionImageCount;
        // Assert.
        actualValue.ShouldBe(0);
    }

    [Fact]
    public void SessionImageCount_ShouldChangeSessionParametersServiceValue_WhenValueChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.SessionImageCount.Returns(0);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            SessionImageCount = 121
        };
        // Assert.
        _mockSharedSessionParametersService.Received().SessionImageCount = 121;
    }

    [Fact]
    public void IsShuffleEnabled_ShouldGetValueFromSharedSessionParametersService_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsShuffleEnabled.Returns(false);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.IsShuffleEnabled;
        // Assert.
        actualValue.ShouldBeFalse();
    }

    [Fact]
    public void IsShuffleEnabled_ShouldChangeSessionParametersServiceValue_WhenValueChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsShuffleEnabled.Returns(false);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        mainViewModel.IsShuffleEnabled = true;
        // Assert.
        _mockSharedSessionParametersService.Received().IsShuffleEnabled = true;
    }

    [Fact]
    public void IsEndlessModeEnabled_ShouldGetValueFromSharedSessionParametersService_WhenViewModelIsInitialized()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsEndlessModeEnabled.Returns(false);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.IsEndlessModeEnabled;
        // Assert.
        actualValue.ShouldBeFalse();
    }

    [Fact]
    public void IsEndlessModeEnabled_ShouldChangeSessionParametersServiceValue_WhenValueChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsEndlessModeEnabled.Returns(false);
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            IsEndlessModeEnabled = true
        };
        // Assert.
        _mockSharedSessionParametersService.Received().IsEndlessModeEnabled = true;
    }

    [Fact]
    public void FoldersInfo_ShouldHaveValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        string expectedValue = "Folders: 0 Images: 0";
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        var actualValue = mainViewModel.FoldersInfo;
        // Assert.
        actualValue.ShouldBe(expectedValue);
    }

    [Theory]
    [InlineData(1, 12)]
    [InlineData(3, 43)]
    [InlineData(5, 67)]
    public void FoldersInfo_ShouldChangeValue_WhenFolderCountAndImageCountInfoAreChanged(int folderCount, int imageCountInfo)
    {
        // Arrange.
        string expectedValue = $"Folders: {folderCount} Images: {imageCountInfo}";
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            FolderCount = folderCount,
            ImageCountInfo = imageCountInfo
        };
        // Assert.
        mainViewModel.FoldersInfo.ShouldBe(expectedValue);
    }


    [Fact]
    public void FolderCountAndImageCountInfo_ShouldChange_WhenItemsAddedToReferenceFolders()
    {
        // Arrange.
        var imageFolder1 = new ImageFolder("C:\\TestFolder", "TestFolder", ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg",]);
        var imageFolder2 = new ImageFolder("C:\\TestFolder", "TestFolder", ["C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg",]);
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        mainViewModel.ReferenceFolders.Add(imageFolder1);
        // Assert.
        mainViewModel.FolderCount.ShouldBe(1);
        mainViewModel.ImageCountInfo.ShouldBe(3);
        // Act.
        mainViewModel.ReferenceFolders.Add(imageFolder2);
        // Assert.
        mainViewModel.FolderCount.ShouldBe(2);
        mainViewModel.ImageCountInfo.ShouldBe(5);
    }

    [Fact]
    public void StartSession_ShouldBeDisabled_WhenCanStartSessionIsFalse()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService);
        // Assert.
        mainViewModel.StartSessionCommand.CanExecute(null).ShouldBeFalse();
    }

    [Fact]
    public void StartSession_ShouldBeEnabled_WhenCanStartSessionIsTrue()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        // Act.
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            FolderCount = 4
        };
        // Assert.
        mainViewModel.StartSessionCommand.CanExecute(null).ShouldBeTrue();
    }

    [Fact]
    public void SessionImageCount_ShouldUpdateSessionParametersService_WhenValueIsChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.SessionImageCount.Returns(0);
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            // Act.
            SessionImageCount = 121
        };
        // Assert.
        _mockSharedSessionParametersService.Received().SessionImageCount = 121;
    }

    [Fact]
    public void IsShuffleEnabled_ShouldUpdateSessionParametersService_WhenValueIsChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsShuffleEnabled.Returns(false);
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            // Act.
            IsShuffleEnabled = true
        };
        // Assert.
        _mockSharedSessionParametersService.Received().IsShuffleEnabled = true;
    }

    [Fact]
    public void IsEndlessModeEnabled_ShouldUpdateSessionParametersService_WhenValueIsChanged()
    {
        // Arrange.
        _mockSharedCollectionService.ImageFolders.Returns(new ObservableCollection<ImageFolder>());
        _mockSharedSessionParametersService.IsEndlessModeEnabled.Returns(false);
        var mainViewModel = new MainViewModel(_mockWindowManager, _mockViewModelFactory, _mockUserSettings, _mockThemeService, _mockSharedCollectionService, _mockSharedSessionParametersService)
        {
            // Act.
            IsEndlessModeEnabled = true
        };
        // Assert.
        _mockSharedSessionParametersService.Received().IsEndlessModeEnabled = true;
    }
}


