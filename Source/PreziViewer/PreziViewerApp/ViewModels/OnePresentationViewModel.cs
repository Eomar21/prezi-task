using PreziViewer.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;

namespace PreziViewer.App.ViewModels
{
    public class OnePresentationViewModel : ViewModelBase, IRoutableViewModel, IDisposable
    {
        private readonly Presentation m_Presntation;
        public IScreen HostScreen { get; }

        private CompositeDisposable m_Disposables = new();
        public ReactiveCommand<Unit, Unit> GoToDetailedViewCommand { get; }

        public OnePresentationViewModel(Presentation presentation, IScreen hostScreen)
        {
            m_Presntation = presentation;
            HostScreen = hostScreen;
            GoToDetailedViewCommand = ReactiveCommand.Create(GoToDetailedView).DisposeWith(m_Disposables);
        }

        private void GoToDetailedView()
        {
            HostScreen.Router.Navigate.Execute(new DetailedPresentationViewModel(m_Presntation, HostScreen));
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        public string Title => m_Presntation.Title;
        public string Description => m_Presntation.Description ?? string.Empty;
        public string ThumbnailUrl => m_Presntation.ThumbnailUrl.ToString();
        public string? UrlPathSegment => string.Concat(m_Presntation.Id, "_one_presentation");
    }
}