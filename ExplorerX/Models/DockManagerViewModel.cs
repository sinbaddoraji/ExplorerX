using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ExplorerX.Models;

public class DockManagerViewModel
{
    /// <summary>Gets a collection of all visible documents</summary>
    public ObservableCollection<FileeExplorerControl> Documents { get; private set; }

    public ObservableCollection<object> Anchorables { get; private set; }

    public DockManagerViewModel(IEnumerable<DockWindowViewModel> dockWindowViewModels)
    {
        this.Documents = new ObservableCollection<FileeExplorerControl>();
        this.Anchorables = new ObservableCollection<object>();

        foreach (var document in dockWindowViewModels)
        {
            document.PropertyChanged += DockWindowViewModel_PropertyChanged;
            if (!document.IsClosed)
                this.Documents.Add(new FileeExplorerControl());
        }
    }

    private void DockWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        //DockWindowViewModel document = sender as DockWindowViewModel;

        //if (e.PropertyName == nameof(DockWindowViewModel.IsClosed))
        //{
        //    if (!document.IsClosed)
        //        this.Documents.Add(document);
        //    else
        //        this.Documents.Remove(document);
        //}
    }
}

public class FileExplorerDockWindowViewModel : DockWindowViewModel
{
}