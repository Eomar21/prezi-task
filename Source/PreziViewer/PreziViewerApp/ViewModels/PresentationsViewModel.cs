using PreziViewer.Services.Interface;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace PreziViewer.App.ViewModels
{
    public class PresentationsViewModel : ViewModelBase, IRoutableViewModel, IDisposable
    {
        private readonly CompositeDisposable m_Disposable = new();
        private readonly IPresentationFetcher m_PresentationFetcher;
        public ObservableCollection<OnePresentationViewModel> Presentations { get; } = new();

        public string? UrlPathSegment => "presentations";
        public IScreen HostScreen { get; }

        public PresentationsViewModel(IPresentationFetcher presentationFetcher, IScreen screen)
        {
            m_PresentationFetcher = presentationFetcher;
            HostScreen = screen;
            LoadPresentations().ConfigureAwait(false);
        }

        private async Task LoadPresentations()
        {
            var presentations = await m_PresentationFetcher.GetPresentations();
            var orderedPresentations = presentations.OrderByDescending(x => x.LastModified);
            foreach (var item in orderedPresentations)
            {
                Presentations.Add(new OnePresentationViewModel(item, HostScreen));
            }
        }

        public void Dispose()
        {
            m_Disposable.Dispose();
        }
    }
}