using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExplorerX.Views;

namespace ExplorerX.Models;

public class DockManagerViewModel
{
    /// <summary>Gets a collection of all visible documents</summary>
    public ObservableCollection<FileExplorerControl> Documents { get; }

    public DockManagerViewModel(IEnumerable<DockWindowViewModel> dockWindowViewModels)
    {
        this.Documents = new ObservableCollection<FileExplorerControl>();

        foreach (var document in dockWindowViewModels)
        {
            if (!document.IsClosed)
                this.Documents.Add(new FileExplorerControl());
        }
    }
    
}