using PreziViewer.App.Views;

namespace PreziViewer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PresentationsView PresentationsView { get; }

        public MainWindowViewModel(PresentationsView positionView)
        {
            PresentationsView = positionView;
        }
    }
}