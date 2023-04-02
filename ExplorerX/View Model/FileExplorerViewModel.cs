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
using Directory = ExplorerX.Data.Directory;
using File = ExplorerX.Data.File;

namespace ExplorerX.View_Model
{
    public class FileExplorerViewModel : INotifyPropertyChanged
    {
        private string _directoryPath;

        public List<IExplorerItem> BrowseHistory = new List<IExplorerItem>();
        public string DirectoryPath 
        {
            get => _directoryPath; 
            
            set
            {
                if(System.IO.Directory.Exists(value) && _directoryPath != value)
                {
                    _directoryPath = value;
                    NavigateTo(_directoryPath);
                }

                OnPropertyChanged("DirectoryPath");
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

        private Directory OriginalParent { get; set; }

        public ObservableCollection<IExplorerItem> ExplorerItems { get; set; } = new ObservableCollection<IExplorerItem>();

        public FileExplorerViewModel()
        {
            NavigateBackCommand = new RelayCommand(_ => { NavigateBack(); });
            NavigateForwardCommand = new RelayCommand(_ => { NavigatForward(); });

            var d = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            OriginalParent = new Directory()
            {
                FullName = d.FullName,
                Name = d.Name,
                DateModified = d.LastWriteTime,
                Type = ExplorerItemType.Directory,
            };

            NavigateTo(OriginalParent);
        }

        public void NavigateTo(string path)
        {
            var d = new System.IO.DirectoryInfo(path);
            var newDirectory = new Directory()
            {
                FullName = d.FullName,
                Name = d.Name,
                DateModified = d.LastWriteTime,
                Type = ExplorerItemType.Directory,
            };
            NavigateTo(newDirectory);
        }

        public void NavigateTo(IExplorerItem explorerItem)
        {
            if(explorerItem.Type == ExplorerItemType.Directory) 
            {
                DirectoryPath = explorerItem.FullName;
                BrowseHistory.Add(explorerItem);
                GetFilesAndDirectories();
            }
        }

        public void Navigate()
        {
            NavigateTo(SelectedExplorerItem);
            DirectoryPath = SelectedExplorerItem.FullName;

            BrowseIndex = BrowseHistory.Count - 1;
        }

        public ICommand NavigateBackCommand { get; set;  } 
        public ICommand NavigateForwardCommand { get; set;  }


        public int BrowseIndex { get; set; } = -1;
        public void NavigateBack()
        {
            if (BrowseIndex == -1)
                BrowseIndex = BrowseHistory.Count - 1;

            if (BrowseIndex > 0)
                BrowseIndex--;

            NavigateTo(BrowseHistory[BrowseIndex]);
        }

        public void NavigatForward()
        {
            if (BrowseIndex < BrowseHistory.Count -1)
                BrowseIndex++;

            if (BrowseHistory.Count > BrowseIndex)
                NavigateTo(BrowseHistory[BrowseIndex]);
        }


        public void GetFilesAndDirectories()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
        
    }
}
