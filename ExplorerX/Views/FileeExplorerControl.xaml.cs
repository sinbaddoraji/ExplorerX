using ExplorerX.Data.Implementations;
using ExplorerX.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExplorerX
{
    /// <summary>
    /// Interaction logic for FileeExplorerControl.xaml
    /// </summary>
    public partial class FileeExplorerControl : UserControl
    {
        public FileExplorerViewModel FileExplorerViewModel { get; set; } = new FileExplorerViewModel();

        public FileeExplorerControl()
        {
            InitializeComponent();
            DataContext = FileExplorerViewModel;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileExplorerViewModel.Navigate();
        }
    }
}
