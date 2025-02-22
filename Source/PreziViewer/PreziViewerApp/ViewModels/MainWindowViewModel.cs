using PreziViewer.Services.Interface;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace PreziViewer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen, IDisposable
    {
        public RoutingState Router { get; }
        public readonly IPresentationFetcher m_PresentationFetcher;
        private string m_StatusText = "Offline";
        private CompositeDisposable m_Disposable = new();
        public ReactiveCommand<Unit, Unit> CloseWindowCommand { get; }
        public ReactiveCommand<Unit, Unit> MinimizeWindowCommand { get; }
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
            CloseWindowCommand = ReactiveCommand.Create(CloseWindow).DisposeWith(m_Disposable);
            MinimizeWindowCommand = ReactiveCommand.Create(MinimizeWindow).DisposeWith(m_Disposable);
            DragWindowCommand = ReactiveCommand.Create<Window>(DragWindow).DisposeWith(m_Disposable);
        }

        private void DragWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.DragMove();
            }
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseWindow()
        {
            Application.Current.Shutdown();
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