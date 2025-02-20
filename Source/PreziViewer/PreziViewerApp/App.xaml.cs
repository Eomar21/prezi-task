using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PreziViewer.App.ViewModels;
using PreziViewer.App.Views;
using PreziViewer.Services;
using ReactiveUI;
using Splat;
using System.Windows;

namespace PreziViewerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost m_Host;
        public IServiceProvider m_ServiceProvider => m_Host.Services;

        public App()
        {
            m_Host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                // Services
                services.WithPresentationCoreServices();

                // Views
                services.AddTransient<MainWindow>();
                services.AddTransient<PresentationsView>();
                services.AddTransient<OnePresentationView>();
                services.AddTransient<DetailedPresentationView>();

                // ViewModels
                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<PresentationsViewModel>();
                services.AddTransient<OnePresentationViewModel>();
                services.AddTransient<DetailedPresentationViewModel>();
            }).Build();
            Locator.CurrentMutable.Register(() => new PresentationsView(), typeof(IViewFor<PresentationsViewModel>));
            Locator.CurrentMutable.Register(() => new OnePresentationView(), typeof(IViewFor<OnePresentationViewModel>));
            Locator.CurrentMutable.Register(() => new DetailedPresentationView(), typeof(IViewFor<DetailedPresentationViewModel>));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await m_Host.StartAsync();
            var mainWindow = m_ServiceProvider.GetRequiredService<MainWindow>();
            var mainViewModel = m_ServiceProvider.GetRequiredService<MainWindowViewModel>();

            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await m_Host.StopAsync();
            m_Host.Dispose();
        }
    }
}