using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Reemit.Gui;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        var name = data?.GetType().FullName?.Replace("ViewModel", "View");
        var type = Type.GetType(name ?? throw new InvalidOperationException());

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data) => data?.GetType().FullName?.EndsWith("ViewModel") == true;
}