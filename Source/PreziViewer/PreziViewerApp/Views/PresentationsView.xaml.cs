using PreziViewer.App.ViewModels;
using System.Windows.Controls;

namespace PreziViewer.App.Views
{
    /// <summary>
    /// Interaction logic for PresentationsView.xaml
    /// </summary>
    public partial class PresentationsView : UserControl
    {
        public PresentationsView(PresentationsViewModel presentationsViewModel)
        {
            InitializeComponent();
            DataContext = presentationsViewModel;
        }
    }
}