using System.Collections.Generic;

namespace ExplorerX.Models
{
    public class MainWindowViewModel
    {
        public DockManagerViewModel DockManagerViewModel { get; private set; }

        public MainWindowViewModel()
        {
            var documents = new List<DockWindowViewModel> { new FileExplorerDockWindowViewModel() };
            this.DockManagerViewModel = new DockManagerViewModel(documents);
        }
    }
}
