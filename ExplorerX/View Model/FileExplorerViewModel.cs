using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ExplorerX.Data.Enums;
using ExplorerX.Data.Implementations;
using ExplorerX.Data.Interfaces;
using Directory = ExplorerX.Data.Implementations.Directory;
using File = ExplorerX.Data.Implementations.File;

namespace ExplorerX.View_Model
{
    public class FileExplorerViewModel : INotifyPropertyChanged
    {

        #region Properties

        public int BrowseIndex { get; set; } = -1;

        private string? _directoryPath;

        public string? DirectoryPath
        {
            get => _directoryPath;

            set
            {
                if (System.IO.Directory.Exists(value) && _directoryPath != value)
                {
                    _directoryPath = value;
                    NavigateTo(_directoryPath);
                }

                OnPropertyChanged("DirectoryPath");
            }
        }

        public string? ShortName { get; set; }

        private ObservableCollection<string> _subDirectoryList = new();

        public ObservableCollection<string> SubDirectoryList
        {
            get => _subDirectoryList;
            set
            {
                _subDirectoryList = value;
                OnPropertyChanged("SubDirectoryList");
            }
        }

        private IExplorerItem? _selectedExplorerItem;

        public IExplorerItem? SelectedExplorerItem
        {
            get => _selectedExplorerItem;
            set
            {
                if (value == null || _selectedExplorerItem == value)
                    return;
                _selectedExplorerItem = value;
                OnPropertyChanged(nameof(SelectedExplorerItem));
            }
        }

        private readonly List<IExplorerItem> _browseHistory = new();

        private string? _searchText;

        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value ?? string.Empty;
                GetFilesAndDirectories();
                OnPropertyChanged(nameof(SearchText));
            }
            
        }

        private Directory OriginalParent { get; set; }

        public ObservableCollection<IExplorerItem> ExplorerItems { get; set; } = new();


        #endregion

        #region Commands

        public ICommand NavigateBackCommand { get; set; }
        public ICommand NavigateForwardCommand { get; set; }
        
        #endregion
        
        #region Navigation and File loading
        
        public void NavigateTo(string? path)
        {
            if (path == null)
                return;

            var d = new DirectoryInfo(path);
            var newDirectory = new Directory
            {
                FullName = d.FullName,
                Name = d.Name,
                DateModified = d.LastWriteTime,
                Type = ExplorerItemType.Directory,
            };

            ShortName = newDirectory.Name;

            NavigateTo(newDirectory);
        }

        public void NavigateTo(IExplorerItem explorerItem)
        {
            try
            {
                if (explorerItem.Type == ExplorerItemType.Directory)
                {
                    DirectoryPath = explorerItem.FullName;
                    _browseHistory.Add(explorerItem);
                    GetFilesAndDirectories();
                }
                else if (explorerItem is { Type: ExplorerItemType.File, FullName: { } })
                {
                    Process.Start(explorerItem.FullName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Navigate()
        {
            if (SelectedExplorerItem == null)
                return;

            NavigateTo(SelectedExplorerItem);
            DirectoryPath = SelectedExplorerItem.FullName;
            BrowseIndex = _browseHistory.Count - 1;
        }

        public void NavigateBack()
        {
            if (BrowseIndex == -1)
                BrowseIndex = _browseHistory.Count - 1;

            if (BrowseIndex > 0)
                BrowseIndex--;

            NavigateTo(_browseHistory[BrowseIndex]);
        }

        public void NavigateForward()
        {
            if (BrowseIndex < _browseHistory.Count - 1)
                BrowseIndex++;

            if (_browseHistory.Count > BrowseIndex)
                NavigateTo(_browseHistory[BrowseIndex]);
        }

        public void GetFilesAndDirectories()
        {
            ExplorerItems.Clear();

            if (DirectoryPath == null)
                return;

            var currentDirectory = new DirectoryInfo(DirectoryPath);
            var parentDirectory = currentDirectory.Parent;
            if (parentDirectory != null)
            {
                ExplorerItems.Add(new Directory
                {
                    FullName = parentDirectory.FullName,
                    Name = "...",
                    DateModified = parentDirectory.LastWriteTime,
                    Type = ExplorerItemType.Directory,
                });
            }
            else
            {
                ExplorerItems.Add(OriginalParent);
            }

            SubDirectoryList.Clear();

            foreach (var d in currentDirectory.GetDirectories(SearchText ?? "*"))
            {
                ExplorerItems.Add(new Directory
                {
                    FullName = d.FullName,
                    Name = d.Name,
                    DateModified = d.LastWriteTime,
                    Type = ExplorerItemType.Directory,
                });

                SubDirectoryList.Add(d.FullName);
            }

            foreach (var f in currentDirectory.GetFiles(SearchText ?? "*"))
            {
                ExplorerItems.Add(new File
                {
                    FullName = f.FullName,
                    Name = f.Name,
                    DateModified = f.LastWriteTime,
                    Type = ExplorerItemType.File,
                    Size = (int)f.Length
                });
            }
        }
        
        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion


        public FileExplorerViewModel()
        {
            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateForwardCommand = new RelayCommand(NavigateForward);

            var d = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            OriginalParent = new Directory
            {
                FullName = d.FullName,
                Name = d.Name,
                DateModified = d.LastWriteTime,
                Type = ExplorerItemType.Directory,
            };

            NavigateTo(OriginalParent);
        }
    }
}
