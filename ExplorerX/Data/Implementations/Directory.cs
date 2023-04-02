using System;
using System.Collections.Generic;
using ExplorerX.Data.Enums;
using ExplorerX.Data.Interfaces;

namespace ExplorerX.Data.Implementations;

public class Directory : IExplorerItem
{
    public string? Name { get; set; }

    public string? FullName { get; set; }

    public List<string>? SubDirectories { get; set; }

    public ExplorerItemType Type { get; set; } = ExplorerItemType.Directory;
    public DateTime DateModified { get; set; }
    public int Size { get; set; }
}