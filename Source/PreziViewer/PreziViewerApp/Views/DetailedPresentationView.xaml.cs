using PreziViewer.App.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
