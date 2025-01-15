﻿using Posetrix.Core.Models;
using Posetrix.Core.Services;
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
    public void PopulateListWithImagePaths_ImagePathsFromImageFolder_ReturnsListOfAllImagePaths()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image1.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateListWithImagePaths(actualList, _imageFolders);
        // Assert.
        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void PopulateListWithImagePaths_ImagePathsFromImageFolder_ReturnsNotEqualListOfAllImagePaths()
    {
        // Arrange.
        List<string> expectedList = ["C:\\TestFolder\\image0.jpg", "C:\\TestFolder\\image2.jpg", "C:\\TestFolder\\image3.jpg", "C:\\TestFolder\\image4.jpg", "C:\\TestFolder\\image5.jpg"];
        // Act.
        List<string> actualList = [];
        ImageCollectionHelpers.PopulateListWithImagePaths(actualList, _imageFolders);
        // Assert.
        Assert.NotEqual(expectedList, actualList);
    }

    [Fact]
    public void ShuffleList_ListOfStrings_ReturnsShuffledList()
    {
        // Arrange.
        List<string> expectedList = ["image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg"];
        // Act.
        List<string> actualList = new(expectedList);
        ImageCollectionHelpers.ShuffleList(actualList);
        // Assert.
        Assert.NotEqual(expectedList, actualList);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void TrimList_ValidTrimValue_ReturnsTrimmedListCount(int count) {
        // Arrange.
        List<string> list = ["image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg"];
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        Assert.Equal(count, list.Count);
    }

    [Theory]
    [InlineData(-2)]
    [InlineData(0)]
    [InlineData(21)]
    public void TrimList_InvalidTrimValue_ReturnsUnchangedListCount(int count)
    {
        // Arrange.
        List<string> list = [ "image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg", "image5.jpg" ];
        var expectedListCount = list.Count;
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        Assert.NotEmpty(list);
        Assert.Equal(expectedListCount, list.Count);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(0)] 
    [InlineData(2)]
    [InlineData(6)]
    public void TrimList_InvalidTrimValueWithEmptyList_ReturnsUnchangedListCount(int count)
    {
        // Arrange.
        List<string> list = [];
        var expectedListCount = list.Count;
        // Act.
        ImageCollectionHelpers.TrimList(list, count);
        // Assert.
        Assert.Empty(list);
        Assert.Equal(expectedListCount, list.Count);
    }
}
