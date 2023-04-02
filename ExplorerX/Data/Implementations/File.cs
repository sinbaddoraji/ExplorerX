using System;
using ExplorerX.Data.Enums;
using ExplorerX.Data.Interfaces;

namespace ExplorerX.Data.Implementations
{
    public class File : IExplorerItem
    {
        public string? Name { get; set; }

        public string? FullName { get; set; }

        public ExplorerItemType Type { get; set; } = ExplorerItemType.File;

        public DateTime DateModified { get; set; }
        public int Size { get; set; }
    }
}
