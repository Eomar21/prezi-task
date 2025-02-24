using PreziViewer.App.ViewModels;
using ReactiveUI;
using System.Windows.Controls;

namespace PreziViewer.App.Views
{
    /// <summary>
    /// Interaction logic for PresentationsView.xaml
    /// </summary>
    public partial class PresentationsView : UserControl, IViewFor<PresentationsViewModel>
    {
        public PresentationsView()
        {
            InitializeComponent();
        }

        public PresentationsViewModel? ViewModel
        {
            get => (PresentationsViewModel)DataContext;
            set => DataContext = value;
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as PresentationsViewModel;
        }
    }
}