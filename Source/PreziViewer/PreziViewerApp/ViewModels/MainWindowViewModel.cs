using PreziViewer.Services.Interface;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Windows;

namespace PreziViewer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen, IDisposable
    {
        public RoutingState Router { get; }
        public readonly IPresentationFetcher m_PresentationFetcher;
        private string m_StatusText = "Offline";
        private CompositeDisposable m_Disposable = new();
        public ReactiveCommand<Window, Unit> DragWindowCommand { get; }

        public string StatusText
        {
            get => m_StatusText;
            set => this.RaiseAndSetIfChanged(ref m_StatusText, value);
        }

        public MainWindowViewModel(IPresentationFetcher presentationFetcher)
        {
            Router = new RoutingState();
            m_PresentationFetcher = presentationFetcher;
            NavigateToPresentations();
            var isSourceOnline = Observable.FromEventPattern<bool>(
                h => m_PresentationFetcher.IsSourceOnline += h,
                h => m_PresentationFetcher.IsSourceOnline -= h).Select(e => e.EventArgs);
            isSourceOnline.Subscribe(x =>
            {
                StatusText = x ? "Online" : "Offline";
            }).DisposeWith(m_Disposable);

            DragWindowCommand = ReactiveCommand.Create<Window>(DragWindow);
        }

        private void DragWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.DragMove();
            }
        }

        public void NavigateToPresentations()
        {
            Router.Navigate.Execute(new PresentationsViewModel(m_PresentationFetcher, this));
        }

        public void Dispose()
        {
            m_Disposable.Dispose();
        }
    }
}