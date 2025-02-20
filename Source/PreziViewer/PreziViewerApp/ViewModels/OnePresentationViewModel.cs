using PreziViewer.Models;

namespace PreziViewer.App.ViewModels
{
    public class OnePresentationViewModel : ViewModelBase
    {
        private Presentation m_Presntation;

        public OnePresentationViewModel(Presentation presentation)
        {
            m_Presntation = presentation;
        }

        public string Title => m_Presntation.Title;
        public string Description => m_Presntation.Description;
    }
}