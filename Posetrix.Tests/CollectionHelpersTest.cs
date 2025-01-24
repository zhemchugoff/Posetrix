using Posetrix.Core.Models;
using Posetrix.Core.Services;
using Shouldly;
using System.Collections.ObjectModel;

namespace Posetrix.Tests;

public class CollectionHelpersTest
{
    private readonly ImageFolder _imageFolder1;
    private readonly ImageFolder _imageFolder2;
    private readonly ObservableCollection<ImageFolder> _imageFolders;

    public CollectionHelpersTest()
    {
        _imageFolders = [];

        _imageFolder1 = new ImageFolder("C:\\TestFolder", "TestFolder",
    [
        "C:\\TestFolder\\image1.jpg",
                "C:\\TestFolder\\image2.jpg",
                "C:\\TestFolder\\image3.jpg",
            ]);
        _imageFolder2 = new ImageFolder("C:\\TestFolder", "TestFolder",
            [
                "C:\\TestFolder\\image4.jpg",
                "C:\\TestFolder\\image5.jpg",
            ]);

        _imageFolders = [_imageFolder1, _imageFolder2];

    }

    [Fact]
    public void PopulateListWithImagePaths_ShouldReturnListOfAllImagePaths_WhenGivenImagePathsFromImageFolder()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateListWithImagePaths(actualList, _imageFolders);
        // Assert.
        actualList.ShouldBeEquivalentTo(expectedList);
    }

    [Fact]
    public void PopulateListWithImagePaths_ShouldReturnFalse_WhenImagePathsFromImageFolderAreNotEqual()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image0.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateListWithImagePaths(actualList, _imageFolders);
        // Assert.
        actualList.ShouldNotBeSameAs(expectedList);
    }

    [Fact]
    public void ShuffleList_ShouldReturnShuffledList_WhenInputIsListOfStrings()
    {
        // Arrange.
        List<string> expectedList = ["image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg"];
        // Act.
        List<string> actualList = new(expectedList);
        ImageCollectionHelpers.ShuffleList(actualList);
        // Assert.
        actualList.ShouldNotBeSameAs(expectedList);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void TrimList_ShouldReturnTrimmedListCount_WhenTrimValueIsValid(int count)
    {
        // Arrange.
        List<string> list = ["image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg"];
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        count.ShouldBe(list.Count);
    }

    [Theory]
    [InlineData(-2)]
    [InlineData(0)]
    [InlineData(21)]
    public void TrimList_ShouldReturnUnchangedListCount_WhenTrimValueIsInvalid(int count)
    {
        // Arrange.
        List<string> list = ["image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg"];
        var expectedListCount = list.Count;
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        list.ShouldNotBeEmpty();
        list.Count.ShouldBe(expectedListCount);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(6)]
    public void TrimList_ShouldReturnUnchangedListCount_WhenListIsEmpty(int count)
    {
        // Arrange.
        List<string> list = [];
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        list.ShouldBeEmpty();
        list.Count.ShouldBe(0);
    }

    [Fact]
    public void PopulateAndConvertObservableColletionToList_ShouldReturnListOfStrings_WhenGivenObservableCollectionWithImageCountNull()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateAndConvertObservableColletionToList(actualList, _imageFolders, false, null);
        // Assert.
        actualList.ShouldBeEquivalentTo(expectedList);
    }

    [Fact]
    public void PopulateAndConvertObservableColletionToList_ShouldReturnNotEqualListOfStrings_WhenGivenObservableCollectionWithImageCountNullAndIsShuffleTrue()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateAndConvertObservableColletionToList(actualList, _imageFolders, true, null);
        // Assert.
        actualList.ShouldNotBeSameAs(expectedList);
    }
}
