using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class UnitTest1
{
    [Fact]
    public void TestIncrement()
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        Assert.Equal(0, viewModel.Counter);
        viewModel.Increment();
        Assert.Equal(1, viewModel.Counter);
    }

    [Fact]
    public void TestDecrement()
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        Assert.Equal(0, viewModel.Counter);
        viewModel.Decrement();
        Assert.Equal(-1, viewModel.Counter);
    }
}
