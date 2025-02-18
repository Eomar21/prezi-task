using System.Reactive.Disposables;

namespace PreziViewer.App.ViewModels
{
    public class PresentationsViewModel : ViewModelBase, IDisposable
    {
        private readonly CompositeDisposable m_Disposable = new();

        public PresentationsViewModel()
        {
        }

        public void Dispose()
        {
            m_Disposable.Dispose();
        }
    }
}