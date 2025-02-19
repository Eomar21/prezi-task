using Moq;
using PreziViewer.Models;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services.NUnit
{
    public class PresentationFetcherTest
    {
        private Mock<IPresentationLocalFetcher> m_PresentationLocalFetcher;
        private Mock<IPresentationOnlineFetcher> m_PresentationOnlineFetcher;
        private PresentationFetcher m_PresentationFetcher;

        [SetUp]
        public void Setup()
        {
            m_PresentationLocalFetcher = new Mock<IPresentationLocalFetcher>();
            m_PresentationOnlineFetcher = new Mock<IPresentationOnlineFetcher>();
            m_PresentationFetcher = new PresentationFetcher(m_PresentationLocalFetcher.Object, m_PresentationOnlineFetcher.Object);
        }

        [Test]
        public async Task GetPresentations_ShouldReturnOnlinePresentations_WhenOnlineDataIsAvailable()
        {
            // Arrange
            var onlinePresentationsList = new List<Presentation>
            {
                new Presentation { Id = Guid.NewGuid(), Title = "Online Presentation 1" },
                new Presentation { Id = Guid.NewGuid(), Title = "Online Presentation 2" }
            };
            var onlinePresentations = new Presentations(onlinePresentationsList);

            m_PresentationOnlineFetcher.Setup(x => x.TryGetOnlinePresentationsAndSave()).ReturnsAsync(onlinePresentations);

            // Act
            var result = await m_PresentationFetcher.GetPresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Online Presentation 1"));
        }

        [Test]
        public async Task GetPresentations_ShouldReturnLocalPresentations_WhenNoOnlineDataIsAvailable()
        {
            // Arrange
            var localPresentationsList = new List<Presentation>
            {
                new Presentation { Id = Guid.NewGuid(), Title = "Local Presentation 1" },
                new Presentation { Id = Guid.NewGuid(), Title = "Local Presentation 2" }
            };
            var localPresentations = new Presentations(localPresentationsList);

            m_PresentationOnlineFetcher.Setup(x => x.TryGetOnlinePresentationsAndSave()).ReturnsAsync(Presentations.Empty);
            m_PresentationLocalFetcher.Setup(x => x.TryGetLocalPresentations()).ReturnsAsync(localPresentations);

            // Act
            var result = await m_PresentationFetcher.GetPresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Local Presentation 1"));
        }

        [Test]
        public async Task GetPresentations_ShouldReturnEmptyList_WhenNoDataIsAvailable()
        {
            // Arrange
            m_PresentationOnlineFetcher.Setup(x => x.TryGetOnlinePresentationsAndSave()).ReturnsAsync(Presentations.Empty);
            m_PresentationLocalFetcher.Setup(x => x.TryGetLocalPresentations()).ReturnsAsync(Presentations.Empty);

            // Act
            var result = await m_PresentationFetcher.GetPresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}
