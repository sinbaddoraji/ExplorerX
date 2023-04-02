namespace ExplorerX.Models;

public abstract class DockWindowViewModel : BaseViewModel
{
    private bool _isClosed;
    public bool IsClosed
    {
        get => _isClosed;
        set
        {
            if (_isClosed == value) 
                return;

            _isClosed = value;
            OnPropertyChanged(nameof(IsClosed));
        }
    }
    
    private bool _canClose;
    public bool CanClose
    {
        get => _canClose;
        set
        {
            if (_canClose == value) 
                return;

            _canClose = value;
            OnPropertyChanged(nameof(CanClose));
        }
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set
        {
            if (_title == value) 
                return;

            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    protected DockWindowViewModel()
    {
        this.CanClose = true;
        this.IsClosed = false;
    }
}