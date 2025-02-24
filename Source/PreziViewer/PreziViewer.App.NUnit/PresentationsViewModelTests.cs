using Moq;
using PreziViewer.App.ViewModels;
using PreziViewer.Models;
using PreziViewer.Services.Interface;
using ReactiveUI;
using System.Reactive.Linq;

namespace PreziViewer.Tests.ViewModels
{
    [TestFixture]
    public class PresentationsViewModelTests : IDisposable
    {
        private Mock<IPresentationFetcher> m_MockPresentationFetcher;
        private Mock<IScreen> m_MockScreen;
        private PresentationsViewModel m_ViewModel;

        [SetUp]
        public void Setup()
        {
            m_MockPresentationFetcher = new Mock<IPresentationFetcher>();
            m_MockScreen = new Mock<IScreen>();

            var mockPresentations = new List<Presentation>
            {
                new() { LastModified = DateTime.UtcNow.AddDays(-1), Title = "Presentation 2", Id=new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg") },
                new() { LastModified = DateTime.UtcNow.AddDays(-9), Title = "Presentation 3", Id=new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg")  },
                new() { LastModified = DateTime.UtcNow.AddDays(-10), Title = "Presentation 4", Id=new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg")  },
                new() { LastModified = DateTime.UtcNow, Title = "Presentation 1",  Id=new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg")  }
            };

            m_MockPresentationFetcher.Setup(x => x.GetPresentations()).Returns(Task.FromResult<IEnumerable<Presentation>>(mockPresentations));

            m_ViewModel = new PresentationsViewModel(m_MockPresentationFetcher.Object, m_MockScreen.Object);
        }

        [TearDown]
        public void TearDown()
        {
            m_ViewModel.Dispose();
        }

        [Test]
        public async Task LoadPresentations_ShouldPresentAllPresentationsWithMostRecentOrder()
        {
            await Task.Delay(100);

            Assert.That(m_ViewModel.Presentations, Has.Count.EqualTo(4));

            Assert.That(m_ViewModel.Presentations.First().Title, Is.EqualTo("Presentation 1"));
            Assert.That(m_ViewModel.Presentations.Last().Title, Is.EqualTo("Presentation 4"));
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