using PreziViewer.App.ViewModels;
using ReactiveUI;
using System.Windows.Controls;

namespace PreziViewer.App.Views
{
    /// <summary>
    /// Interaction logic for DetailedPresentationView.xaml
    /// </summary>
    public partial class DetailedPresentationView : UserControl, IViewFor<DetailedPresentationViewModel>
    {
        public DetailedPresentationView()
        {
            InitializeComponent();
        }

        public DetailedPresentationViewModel? ViewModel
        {
            get => (DetailedPresentationViewModel)DataContext;
            set => DataContext = value;
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DetailedPresentationViewModel)value;
        }
    }
}