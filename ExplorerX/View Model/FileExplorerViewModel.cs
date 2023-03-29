using ExplorerX.Data;
using ExplorerX.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using File = ExplorerX.Data.File;

namespace ExplorerX.View_Model
{
    public class FileExplorerViewModel : INotifyPropertyChanged
    {
        private string _directoryPath = @"C:\";

        public List<IExplorerItem> BrowseHistory = new List<IExplorerItem>();
        public string DirectoryPath 
        {
            get => _directoryPath; 
            
            set
            {
                if(System.IO.Directory.Exists(value))
                {
                    _directoryPath = value;
                    GetFilesAndDirectories();

                    BrowseIndex++;
                }
            }
        }

        private IExplorerItem _selectedExplorerItem;

        public event PropertyChangedEventHandler? PropertyChanged;

        public IExplorerItem SelectedExplorerItem
        {
            get { return _selectedExplorerItem; }
            set
            {
                if (_selectedExplorerItem != value && value != null)
                {
                    _selectedExplorerItem = value;
                    OnPropertyChanged(nameof(SelectedExplorerItem));

                   // DirectoryPath = _selectedExplorerItem.FullName;
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string SearchText { get; set; }

        public ObservableCollection<IExplorerItem> ExplorerItems { get; set; } = new ObservableCollection<IExplorerItem>();

        public FileExplorerViewModel()
        {
            GetFilesAndDirectories();

            //NavigateBackCommand = new RelayCommand(NavigateBack);
            //NavigateForwardCommand = new RelayCommand(NavigateForward, () => BrowseIndex < BrowseHistory.Count - 1);

        }

        public void NavigateTo(IExplorerItem explorerItem)
        {
            if(explorerItem != null && explorerItem.Type == ExplorerItemType.Directory) 
            {
                DirectoryPath = explorerItem.FullName;
            }
        }

        public void Navigate()
        {
            NavigateTo(SelectedExplorerItem);

            if (BrowseHistory.Count > BrowseIndex)
                BrowseHistory.RemoveRange(BrowseIndex, BrowseHistory.Count - BrowseIndex);
        }
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateForwardCommand { get; }


        public int BrowseIndex { get; set; } = 0;
        public void NavigateBack()
        {
            if (BrowseIndex > 0)
                BrowseIndex--;

            NavigateTo(BrowseHistory[BrowseIndex]);
        }

        public void NavigatForward()
        {
            if (BrowseIndex < BrowseHistory.Count -2)
                BrowseIndex++;

            if (BrowseHistory.Count > BrowseIndex)
                NavigateTo(BrowseHistory[BrowseIndex]);
        }


        public void GetFilesAndDirectories()
        {
            ExplorerItems.Clear();

            foreach (var item in System.IO.Directory.EnumerateDirectories(DirectoryPath))
            {
                DirectoryInfo d = new DirectoryInfo(item);
                ExplorerItems.Add(new File()
                {
                    FullName = d.FullName,
                    Name = d.Name,
                    DateModified = d.LastWriteTime,
                    Type = ExplorerItemType.Directory,
                });
            }

            foreach (var item in System.IO.Directory.EnumerateFiles(DirectoryPath))
            {
                FileInfo f = new FileInfo(item);
                ExplorerItems.Add(new File()
                {
                    FullName = f.FullName,
                    Name = f.Name,
                    DateModified = f.LastWriteTime,
                    Type = ExplorerItemType.File,
                    Size = (int)f.Length
                });
            }
        }
        
    }
}
