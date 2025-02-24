using Microsoft.Reactive.Testing;
using Moq;
using PreziViewer.App.ViewModels;
using PreziViewer.Models;
using ReactiveUI;
using ReactiveUI.Testing;

namespace PreziViewer.Tests.ViewModels
{
    [TestFixture]
    public class DetailedPresentationViewModelTests
    {
        private Mock<IScreen> m_MockScreen;
        private RoutingState m_MockRouter;
        private DetailedPresentationViewModel m_ViewModel;
        private Presentation m_MockPresentation;

        [SetUp]
        public void Setup()
        {
            m_MockScreen = new Mock<IScreen>();
            m_MockRouter = new RoutingState();

            m_MockScreen.SetupGet(s => s.Router).Returns(m_MockRouter);

            m_MockPresentation = new Presentation
            {
                Id = Guid.NewGuid(),
                ThumbnailUrl = new Uri("https://Prezi.com/thumbnail.jpg"),
                Title = "Test Presentation",
                Description = "Test Description",
                Privacy = "Public",
                LastModified = DateTime.UtcNow,
                Owner = new Owner { FirstName = "James", LastName = "Bond" }
            };

            m_ViewModel = new DetailedPresentationViewModel(m_MockPresentation, m_MockScreen.Object);
        }

        [Test]
        public void DetailedPresentationViewModel_ShouldInitializeProperly()
        {
            Assert.That(m_ViewModel.Title, Is.EqualTo("Test Presentation"));
            Assert.That(m_ViewModel.Description, Is.EqualTo("Test Description"));
            Assert.That(m_ViewModel.Privacy, Is.EqualTo("Public"));
            Assert.That(m_ViewModel.LastModifiedDate, Is.EqualTo(m_MockPresentation.LastModified.ToLongDateString()));
            Assert.That(m_ViewModel.LastModifiedTime, Is.EqualTo(m_MockPresentation.LastModified.ToLongTimeString()));
            Assert.That(m_ViewModel.Owner, Is.EqualTo("James Bond"));
            Assert.That(m_ViewModel.UrlPathSegment, Is.EqualTo(m_MockPresentation.Id.ToString()));
        }

        [Test]
        public void GoBackCommand_ShouldNavigateBack()
        {
            new TestScheduler().With(scheduler =>
            {
                //Arrange
                var router = new RoutingState();
                router.NavigationStack.Add(new OnePresentationViewModel(m_MockPresentation, m_MockScreen.Object));
                router.NavigationStack.Add(m_ViewModel);

                m_MockScreen.Setup(x => x.Router).Returns(router);

                //Act
                m_ViewModel.GoBackCommand.Execute().Subscribe();
                scheduler.AdvanceByMs(1);

                // Assert
                var newVm = router.NavigationStack.LastOrDefault();
                Assert.That(newVm, Is.Not.Null);
                Assert.That(newVm, Is.InstanceOf<OnePresentationViewModel>());
            });
        }
    }
}