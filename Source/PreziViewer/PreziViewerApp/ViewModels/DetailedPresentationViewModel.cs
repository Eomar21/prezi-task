using PreziViewer.Models;
using ReactiveUI;
using System.Reactive;

namespace PreziViewer.App.ViewModels
{
    public class DetailedPresentationViewModel : ViewModelBase, IRoutableViewModel
    {
        private Presentation m_Presentation { get; }
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        public DetailedPresentationViewModel(Presentation presentation, IScreen hostScreen)
        {
            m_Presentation = presentation;
            HostScreen = hostScreen;
            GoBackCommand = ReactiveCommand.Create(GoBack);
        }

        private void GoBack()
        {
            HostScreen.Router.NavigateBack.Execute();
        }

        public string Title => m_Presentation.Title;
        public string Description => m_Presentation.Description;

        public string? UrlPathSegment => m_Presentation.Id.ToString();

    }
}
