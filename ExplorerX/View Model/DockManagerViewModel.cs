using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExplorerX.Data.Implementations;
using ExplorerX.Views;

namespace ExplorerX.View_Model;

public class DockManagerViewModel : INotifyPropertyChanged
{
    /// <summary>Gets a collection of all visible documents</summary>
    
    private ObservableCollection<FileExplorerControl> _documents = new();

    public ObservableCollection<FileExplorerControl> Documents
    {
        get => _documents;
        set
        {
            _documents = value;
            OnPropertyChanged();
        }
    }

    public DockManagerViewModel()
    {
        OpenNewTab();
    }

    private void OpenNewTab()
    {
        var newFileExplorerControl = new FileExplorerControl
        {
            FileExplorerViewModel =
            {
                NewTabCommand = new RelayCommand(OpenNewTab)
            }
        };
        newFileExplorerControl.FileExplorerViewModel.Navigate();
        newFileExplorerControl.Title = newFileExplorerControl.FileExplorerViewModel.ShortName;

        Documents.Add(newFileExplorerControl);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}