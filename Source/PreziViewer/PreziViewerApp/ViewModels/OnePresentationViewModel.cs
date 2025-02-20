using PreziViewer.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;

namespace PreziViewer.App.ViewModels
{
    public class OnePresentationViewModel : ViewModelBase, IDisposable
    {
        private Presentation m_Presntation;
        public IScreen HostScreen;
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
        public string Description => m_Presntation.Description;
        public string ThumbnailUrl => m_Presntation.ThumbnailUrl.ToString();
    }
}