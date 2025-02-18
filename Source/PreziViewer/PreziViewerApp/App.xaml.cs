using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PreziViewer.App.ViewModels;
using PreziViewer.App.Views;
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

                // Views
                services.AddTransient<MainWindow>();
                services.AddTransient<PresentationsView>();

                // ViewModels
                services.AddTransient<MainWindowViewModel>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await m_Host.StartAsync();
            var mainWindow = m_ServiceProvider.GetRequiredService<MainWindow>();
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