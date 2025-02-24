using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PreziViewer.Models;
using PreziViewer.Services.Interface;
using System.Net;

namespace PreziViewer.Services.NUnit
{
    public class PresentationOnlineFetcherTest
    {
        private Mock<HttpMessageHandler> m_HttpMessageHandler;
        private Mock<HttpClient>? m_HttpClient;
        private Mock<IConfigurationService> m_ConfigurationService;
        private IPresentationOnlineFetcher? m_PresentationOnlineFetcher;

        [SetUp]
        public void Setup()
        {
            m_HttpMessageHandler = new Mock<HttpMessageHandler>();
            m_ConfigurationService = new Mock<IConfigurationService>();
            m_ConfigurationService.Setup(x => x.GetString("LoggingLocation")).Returns("appsettings.json");
            m_ConfigurationService.Setup(x => x.GetString("OnlineRepo")).Returns("https://s3.amazonaws.com/prezi-desktop/other/Assesment/prezilist.json");
        }

        public void UseOnlineData()
        {
            m_HttpClient = new Mock<HttpClient>();
            m_PresentationOnlineFetcher = new PresentationOnlineFetcher(m_HttpClient.Object, m_ConfigurationService.Object);
        }

        public void UseMessageHandlerWithFakeData()
        {
            m_HttpClient = new Mock<HttpClient>(m_HttpMessageHandler.Object);
            m_PresentationOnlineFetcher = new PresentationOnlineFetcher(m_HttpClient.Object, m_ConfigurationService.Object);
        }

        [Test]
        public async Task TryGetOnlinePresentations_ShouldReturnExpectedDataIfUsedOnlineSource()
        {
            // Arrange
            UseOnlineData();

            // Act
            Assert.That(m_PresentationOnlineFetcher, Is.Not.Null);
            Presentations? result = await m_PresentationOnlineFetcher.TryGetOnlinePresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.List.Count(), Is.EqualTo(10));
            Assert.That(result.List.First().Id, Is.EqualTo(new Guid("c1f2fb37-590a-4055-b21d-f5732c73d481")));
            Assert.That(result.List.First().Title, Is.EqualTo("Untitled Presentation"));
        }

        [Test]
        public async Task TryGetOnlinePresentationsAndSave_ShouldReturnExpectedDataIfUsedOnlineSourceAndSave()
        {
            // Arrange
            UseOnlineData();

            // Act
            Assert.That(m_PresentationOnlineFetcher, Is.Not.Null);
            var result = await m_PresentationOnlineFetcher.TryGetOnlinePresentationsAndSave();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.List.Count(), Is.EqualTo(10));
            Assert.That(result.List.First().Id, Is.EqualTo(new Guid("c1f2fb37-590a-4055-b21d-f5732c73d481")));
            Assert.That(result.List.First().Title, Is.EqualTo("Untitled Presentation"));
        }

        [Test]
        public async Task TryGetOnlinePresentations_ShouldReturnDeserializedPresentations_WhenApiReturnsValidJson()
        {
            // Arrange
            UseMessageHandlerWithFakeData();
            var expectedPresentationsList = new List<Presentation>
            {
                new Presentation { Id = Guid.NewGuid(), Title = "Presentation 1", Description = "Desc 1", ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg") },
                new Presentation { Id = Guid.NewGuid(), Title = "Presentation 2", Description = "Desc 2", ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg") }
            };

            var expectedPresentations = new Presentations(expectedPresentationsList);

            string? jsonResponse = JsonConvert.SerializeObject(expectedPresentations);

            m_HttpMessageHandler
                .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(jsonResponse)
                    });

            // Act
            Assert.That(m_PresentationOnlineFetcher, Is.Not.Null);
            var result = await m_PresentationOnlineFetcher.TryGetOnlinePresentations();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.List.Count(), Is.EqualTo(2));
            Assert.That(result.List.First().Id, Is.EqualTo(expectedPresentations.List.First().Id));
            Assert.That(result.List.First().Title, Is.EqualTo(expectedPresentations.List.First().Title));
        }

        [Test]
        public async Task TryGetOnlinePresentations_ShouldReturnEmptyList_WhenApiReturnsInvalidJson()
        {
            // Arrange
            UseMessageHandlerWithFakeData();
            string? invalidJson = "{ invalid_json_data }";

            m_HttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(invalidJson)
                });

            // Act
            Assert.That(m_PresentationOnlineFetcher, Is.Not.Null);
            var result = await m_PresentationOnlineFetcher.TryGetOnlinePresentations();

            // Assert
            Assert.IsNotNull(result.List);
            Assert.IsEmpty(result.List); // Should return an empty list if deserialization fails
        }

        [Test]
        public async Task TryGetOnlinePresentations_ShouldReturnEmptyList_WhenNetworkErrorOccurs()
        {
            // Arrange
            UseMessageHandlerWithFakeData();
            m_HttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            Assert.That(m_PresentationOnlineFetcher, Is.Not.Null);
            var result = await m_PresentationOnlineFetcher.TryGetOnlinePresentations();

            // Assert
            Assert.IsNotNull(result.List);
            Assert.IsEmpty(result.List); // Should return an empty list on network failure
        }
    }
}