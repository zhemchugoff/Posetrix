using NSubstitute;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.ViewModels;
using System.Collections.ObjectModel;

namespace Posetrix.Tests;

public class FolderAddViewModelTests
{
    private readonly IFolderBrowserServiceAsync _mockFolderBrowserService;
    private readonly IExtensionsService _mockExtensionsService;
    private readonly ISharedCollectionService _mockSharedCollectionService;

    public FolderAddViewModelTests()
    {
        _mockFolderBrowserService = Substitute.For<IFolderBrowserServiceAsync>();
        _mockExtensionsService = Substitute.For<IExtensionsService>();
        _mockSharedCollectionService = Substitute.For<ISharedCollectionService>();
    }

    [Fact]
    public void WindowTitle_ShouldNotBeEmpty()
    {
        // Act.
        string actualWindowTitle = FolderAddViewModel.WindowTitle;
        // Assert.
        Assert.NotEmpty(actualWindowTitle);
    }
    [Fact]
    public void WindowTitle_ShouldHaveConstValue()
    {
        // Arrange.
        string expectedWindowTitle = "Add folders";
        // Act.
        string actualWindowTitle = FolderAddViewModel.WindowTitle;
        // Assert.
        Assert.Equal(expectedWindowTitle, actualWindowTitle);
    }

    [Fact]
    public void ViewModelName_ShouldHasConstantValue_WhenViewModelIsInitialized()
    {
        // Arrange.
        var expectedViewModelName = ViewModelNames.FolderAdd;
        // Act.
        var folderAddViewModel = new FolderAddViewModel(_mockFolderBrowserService, _mockExtensionsService, _mockSharedCollectionService);
        var actualViewModelName = folderAddViewModel.ViewModelName;
        // Assert.
        Assert.Equal(expectedViewModelName, actualViewModelName);
    }

    [Fact]
    public void Folders_ShouldBeEqualToSharedCollection_WhenViewModelIsInitialized()
    {
        // Arrange.
        ImageFolder imageFolder1 = new("C:\\TestFolder1", "TestFolder1",
            ["C:\\TestFolder1\\image1.jpg", "C:\\TestFolder1\\image2.jpg", "C:\\TestFolder1\\image3.jpg",]);
        ImageFolder imageFolder2 = new("C:\\TestFolder2", "TestFolder2",
            ["C:\\TestFolder2\\image4.jpg", "C:\\TestFolder2\\image5.jpg",]);
        ObservableCollection<ImageFolder> expectedCollection = [imageFolder1, imageFolder2];
        _mockSharedCollectionService.ImageFolders.Returns(expectedCollection);
        // Act.
        var folderAddViewModel = new FolderAddViewModel(_mockFolderBrowserService, _mockExtensionsService, _mockSharedCollectionService);
        var actualCollection = folderAddViewModel.Folders;
        // Assert.
        Assert.Equal(expectedCollection, actualCollection);
    }

    [Fact]
    public void RemoveFolder_ShouldBeDisabled_WhenSelectedFolderIsNull()
    {
        // Arrange.
        var folderAddViewModel = new FolderAddViewModel(_mockFolderBrowserService, _mockExtensionsService, _mockSharedCollectionService)
        {
            SelectedFolder = null
        };
        // Act.
        if (folderAddViewModel.RemoveFolderCommand.CanExecute(null))
        {
            folderAddViewModel.RemoveFolderCommand.Execute(null);
        }
        // Assert.
        Assert.False(folderAddViewModel.RemoveFolderCommand.CanExecute(null));
    }

    [Fact]
    public void RemoveFolder_ShouldBeEnabled_WhenSelectedFolderIsNotNull()
    {
        // Arrange.
        var folderAddViewModel = new FolderAddViewModel(_mockFolderBrowserService, _mockExtensionsService, _mockSharedCollectionService);
        ImageFolder folder = new("C:\\Folder1", "Folder1", ["file1", "file2"]);
        folderAddViewModel.SelectedFolder = folder;
        // Act.
        if (folderAddViewModel.RemoveFolderCommand.CanExecute(null))
        {
            folderAddViewModel.RemoveFolderCommand.Execute(null);
        }
        // Assert.
        Assert.True(folderAddViewModel.RemoveFolderCommand.CanExecute(null));
    }
}
