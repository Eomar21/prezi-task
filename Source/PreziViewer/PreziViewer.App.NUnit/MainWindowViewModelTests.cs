using Moq;
using PreziViewer.App.ViewModels;
using PreziViewer.Services.Interface;

namespace PreziViewer.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        private Mock<IPresentationFetcher> m_PresentationFetcherMock;
        private MainWindowViewModel m_ViewModel;

        [SetUp]
        public void Setup()
        {
            m_PresentationFetcherMock = new Mock<IPresentationFetcher>();
            m_ViewModel = new MainWindowViewModel(m_PresentationFetcherMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            m_ViewModel.Dispose();
        }

        [Test]
        public void Constructor_Should_NavigateToPresentations()
        {
            var lastVM = m_ViewModel.Router.NavigationStack.LastOrDefault();
            Assert.IsNotNull(lastVM);
            Assert.IsInstanceOf<PresentationsViewModel>(lastVM);
        }

        [Test]
        public void StatusText_Should_Update_When_IsSourceOnline_Event_Raised()
        {
            Assert.That(m_ViewModel.StatusText, Is.EqualTo("Offline"));

            m_PresentationFetcherMock.Raise(p => p.IsSourceOnline += null, m_PresentationFetcherMock.Object, true);
            Assert.That(m_ViewModel.StatusText, Is.EqualTo("Online"));

            m_PresentationFetcherMock.Raise(p => p.IsSourceOnline += null, m_PresentationFetcherMock.Object, false);
            Assert.That(m_ViewModel.StatusText, Is.EqualTo("Offline"));
        }
    }
}