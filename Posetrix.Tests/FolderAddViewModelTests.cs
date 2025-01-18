using NSubstitute;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

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

}
