using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);

        // Replace "ViewModel" with "View" and adjust the namespace.
        var viewName = name
            .Replace("Posetrix.Core", "Posetrix.Avalonia") // Adjust namespaces
            .Replace("ViewModel", "View");

        var type = Type.GetType(viewName);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + viewName };
    }

    public bool Match(object? data)
    {
        return data is DynamicViewModel;
    }
}