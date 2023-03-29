using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerX.Data.Implementations
{
    public interface IExplorerItem
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }

        public DateTime DateModified { get; set; }

        public ExplorerItemType Type { get; set; }

        public int Size { get; set; }
    }

    public enum ExplorerItemType
    {
        File,
        Directory
    }
    
}
