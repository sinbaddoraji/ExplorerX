using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerX.Models
{
    public class MainWindowViewModel
    {
        public DockManagerViewModel DockManagerViewModel { get; private set; }
        //public MenuViewModel MenuViewModel { get; private set; }

        public MainWindowViewModel()
        {
            var documents = new List<DockWindowViewModel>();

            for (int i = 0; i < 6; i++)
                documents.Add(new FileExplorerDockWindowViewModel() { Title = "C:\\ " + i.ToString() });

            this.DockManagerViewModel = new DockManagerViewModel(documents);
            //this.MenuViewModel = new MenuViewModel(documents);
        }
    }
}
