using PreziViewer.Models;
using ReactiveUI;
using System.Reactive;

namespace PreziViewer.App.ViewModels
{
    public class OnePresentationViewModel : ViewModelBase
    {
        private Presentation m_Presntation;
        public IScreen HostScreen;
        public ReactiveCommand<Unit, Unit> GoToDetailedViewCommand { get; }

        public OnePresentationViewModel(Presentation presentation, IScreen hostScreen)
        {
            m_Presntation = presentation;
            HostScreen = hostScreen;
            GoToDetailedViewCommand = ReactiveCommand.Create(GoToDetailedView);
        }

        private void GoToDetailedView()
        {
            HostScreen.Router.Navigate.Execute(new DetailedPresentationViewModel(m_Presntation, HostScreen));
        }

        public string Title => m_Presntation.Title;
        public string Description => m_Presntation.Description;
        public string ThumbnailUrl => m_Presntation.ThumbnailUrl.ToString();


    }
}