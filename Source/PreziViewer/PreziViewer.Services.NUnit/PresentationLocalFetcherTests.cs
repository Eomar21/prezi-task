using Moq;
using Newtonsoft.Json;
using PreziViewer.Models;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services.NUnit
{
    public class PresentationLocalFetcherTest
    {
        private Mock<IConfigurationService> m_ConfigurationService;
        private IPresentationLocalFetcher m_PresentationLocalFetcher;
        private string testFilePath;

        [SetUp]
        public void Setup()
        {
            m_ConfigurationService = new Mock<IConfigurationService>();
            testFilePath = Path.Combine(AppContext.BaseDirectory, "test_appsettings.json");
            m_ConfigurationService.Setup(x => x.GetString("LoggingLocation")).Returns("test_appsettings.json");
            m_PresentationLocalFetcher = new PresentationLocalFetcher(m_ConfigurationService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [Test]
        public async Task TryGetOnlinePresentations_ShouldReturnPresentations_WhenValidJsonExists()
        {
            // Arrange
            var expectedPresentationsList = new List<Presentation>
            {
                new Presentation { Id = Guid.NewGuid(), Title = "Fake Presentation 1", Description = "Fake Desc 1" },
                new Presentation { Id = Guid.NewGuid(), Title = "Fake Presentation 2", Description = "Fake Desc 2" }
            };
            var expectedPresentations = new Presentations(expectedPresentationsList);
            File.WriteAllText(testFilePath, JsonConvert.SerializeObject(expectedPresentations));

            // Act
            var result = await m_PresentationLocalFetcher.TryGetLocalPresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.List.Count, Is.EqualTo(2));
            Assert.That(result.List.First().Title, Is.EqualTo("Fake Presentation 1"));
            Assert.That(result.List.Last().Title, Is.EqualTo("Fake Presentation 2"));
        }

        [Test]
        public async Task TryGetLocalPresentations_ShouldReturnEmptyList_WhenFileDoesNotExist()
        {
            // Ensure file does not exist
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            // Act
            var result = await m_PresentationLocalFetcher.TryGetLocalPresentations();

            // Assert
            Assert.IsNotNull(result.List);
            Assert.IsEmpty(result.List);
        }

        [Test]
        public async Task TryGetLocalPresentations_ShouldReturnEmptyList_WhenFileIsEmpty()
        {
            // Arrange
            File.WriteAllText(testFilePath, ""); // Create an empty file

            // Act
            var result = await m_PresentationLocalFetcher.TryGetLocalPresentations();

            // Assert
            Assert.IsNotNull(result.List);
            Assert.IsEmpty(result.List);
        }

        [Test]
        public async Task TryGetLocalPresentations_ShouldReturnEmptyList_WhenFileContainsInvalidJson()
        {
            // Arrange
            File.WriteAllText(testFilePath, "{ invalid_json_data }");

            // Act
            var result = await m_PresentationLocalFetcher.TryGetLocalPresentations();

            // Assert
            Assert.IsNotNull(result.List);
            Assert.IsEmpty(result.List);
        }
    }
}
