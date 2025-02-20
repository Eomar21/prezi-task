using PreziViewer.Services.Interface;
using ReactiveUI;

namespace PreziViewer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; }
        public readonly IPresentationFetcher m_PresentationFetcher;

        public MainWindowViewModel(IPresentationFetcher presentationFetcher)
        {
            Router = new RoutingState();
            m_PresentationFetcher = presentationFetcher;
            NavigateToPresentations();
        }

        public void NavigateToPresentations()
        {
            Router.Navigate.Execute(new PresentationsViewModel(m_PresentationFetcher, this));
        }
    }
}