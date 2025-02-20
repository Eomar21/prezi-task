using PreziViewer.Models;
using PreziViewer.Services.Interface;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Threading.Tasks;

namespace PreziViewer.App.ViewModels
{
    public class PresentationsViewModel : ViewModelBase, IDisposable
    {
        private readonly CompositeDisposable m_Disposable = new();
        private readonly IPresentationFetcher m_PresentationFetcher;
        public ObservableCollection<Presentation> Presentations { get; } = new();



        public PresentationsViewModel(IPresentationFetcher presentationFetcher)
        {
            m_PresentationFetcher = presentationFetcher;
            LoadPresentations();
        }

        private async void LoadPresentations()
        {
            var presentations = await m_PresentationFetcher.GetPresentations();
            foreach (var item in presentations)
            {
                Presentations.Add(item);
            }
        }

        public void Dispose()
        {
            m_Disposable.Dispose();
        }
    }
}