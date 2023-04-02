using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ExplorerX.View_Model;

namespace ExplorerX.Views
{
    /// <summary>
    /// Interaction logic for FileExplorerControl.xaml
    /// </summary>
    public partial class FileExplorerControl : INotifyPropertyChanged
    {
        public string? _title;

        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public FileExplorerViewModel FileExplorerViewModel { get; set; } = new FileExplorerViewModel();

        public FileExplorerControl()
        {
            InitializeComponent();
            DataContext = FileExplorerViewModel;
            Title = FileExplorerViewModel.ShortName;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileExplorerViewModel.Navigate();

            Title = FileExplorerViewModel.ShortName;
        }

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
