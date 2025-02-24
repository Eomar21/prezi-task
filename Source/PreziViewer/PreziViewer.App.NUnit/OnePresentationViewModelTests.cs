using Microsoft.Reactive.Testing;
using Moq;
using PreziViewer.App.ViewModels;
using PreziViewer.Models;
using ReactiveUI;
using ReactiveUI.Testing;

namespace PreziViewer.Tests.ViewModels
{
    [TestFixture]
    public class OnePresentationViewModelTests : IDisposable
    {
        private Mock<IScreen> m_MockScreen;
        private OnePresentationViewModel m_ViewModel;
        private Presentation m_MockPresentation;

        [SetUp]
        public void Setup()
        {
            m_MockScreen = new Mock<IScreen>();

            m_MockPresentation = new Presentation
            {
                Title = "Test Presentation",
                Id = Guid.NewGuid(),
                Description = "Test Description",
                ThumbnailUrl = new Uri("https://Prezi.com/thumbnail.jpg")
            };

            m_ViewModel = new OnePresentationViewModel(m_MockPresentation, m_MockScreen.Object);
        }

        [TearDown]
        public void TearDown()
        {
            m_ViewModel.Dispose();
        }

        [Test]
        public void OnePresentationViewModel_ShouldInitializeCorrectly()
        {
            Assert.That(m_ViewModel.Title, Is.EqualTo("Test Presentation"));
            Assert.That(m_ViewModel.Description, Is.EqualTo("Test Description"));
            Assert.That(m_ViewModel.ThumbnailUrl, Is.EqualTo("https://prezi.com/thumbnail.jpg"));
        }

        [Test]
        public void GoToDetailedViewCommand_ShouldNavigateToDetailedViewModel()
        {
            new TestScheduler().With(scheduler =>
            {
                var router = new RoutingState();
                m_MockScreen.Setup(x => x.Router).Returns(router);

                m_ViewModel.GoToDetailedViewCommand.Execute().Subscribe();

                scheduler.AdvanceByMs(1);

                var newVm = router.NavigationStack.LastOrDefault();
                Assert.IsNotNull(newVm);
                Assert.IsInstanceOf<DetailedPresentationViewModel>(newVm);
            });
        }

        [Test]
        public void Dispose_ShouldDisposeCompositeDisposable()
        {
            Assert.DoesNotThrow(() => m_ViewModel.Dispose());
        }

        public void Dispose()
        {
            m_ViewModel?.Dispose();
        }
    }
}