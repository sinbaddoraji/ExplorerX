using ExplorerX.Data.Implementations;
using System;
using System.Collections.Generic;

namespace ExplorerX.Data
{
    public class Directory : IExplorerItem
    {
        public string? Name { get; set; }

        public string? FullName { get; set; }

        public List<File>? Files { get; set; }

        public List<Directory>? Directories { get; set; }

        public ExplorerItemType Type { get; set; } = ExplorerItemType.File;
        public DateTime DateModified { get; set; }
        public int Size { get; set; }
    }
}
