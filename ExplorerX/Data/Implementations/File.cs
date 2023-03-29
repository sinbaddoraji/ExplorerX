using ExplorerX.Data.Implementations;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerX.Data
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
