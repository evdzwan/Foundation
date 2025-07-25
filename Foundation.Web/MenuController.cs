﻿using Foundation.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Concurrent;

namespace Foundation;

public sealed class MenuController : IDisposable
{
    readonly ConcurrentDictionary<string, Menu> Menus = new(StringComparer.OrdinalIgnoreCase);

    public Task HideMenu(string name)
        => Menus.GetValueOrDefault(name) is { } menu ? menu.Hide() : Task.CompletedTask;

    public Task ShowMenu(string name, MouseEventArgs e)
        => Menus.GetValueOrDefault(name) is { } menu ? menu.Show(e) : Task.CompletedTask;

    public Task ShowMenu(string name, object? context, MouseEventArgs e)
        => Menus.GetValueOrDefault(name) is { } menu ? menu.Show(context, e) : Task.CompletedTask;

    internal IDisposable RegisterMenu(Menu menu)
        => menu.Name is { Length: > 0 } name && Menus.TryAdd(name, menu)
            ? new Disposable(() => Menus.TryRemove(name, out _))
            : Disposable.Empty;

    void IDisposable.Dispose()
        => Menus.Clear();
}
