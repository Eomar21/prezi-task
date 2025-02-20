using Newtonsoft.Json;
using PreziViewer.Models;

namespace PreziViewer.Tests
{
    [TestFixture]
    public class PresentationTests
    {
        [Test]
        public void JsonSerialization_ShouldProduceExpectedJson()
        {
            // Arrange
            var presentation = new Presentation
            {
                Id = Guid.NewGuid(),
                Title = "Test Presentation",
                ThumbnailUrl = new Uri("https://0701.static.prezi.com/preview/v2/ongheth6dj2n532tnynjh56wdx6jc3sachvcdoaizecfr3dnitcq_3_0.png"),
                Description = "A test description",
                Privacy = "Public",
                LastModified = new DateTime(2020, 1, 1, 12, 0, 0),
                Owner = new Owner { FirstName = "Test", LastName = "Owner" }
            };

            // Act
            string json = JsonConvert.SerializeObject(presentation);

            StringAssert.Contains("\"Id\"", json);
            StringAssert.Contains("\"Title\":\"Test Presentation\"", json);
            StringAssert.Contains("\"ThumbnailUrl\":\"https://0701.static.prezi.com/preview/v2/ongheth6dj2n532tnynjh56wdx6jc3sachvcdoaizecfr3dnitcq_3_0.png\"", json);
            StringAssert.Contains("\"Description\":\"A test description\"", json);
            StringAssert.Contains("\"Privacy\":\"Public\"", json);
            StringAssert.Contains("\"LastModified\"", json);
            StringAssert.Contains("\"Owner\"", json);
        }

        [Test]
        public void JsonDeserialization_ShouldReturnValidPresentation()
        {
            var json = @"
            {
                ""Id"": ""c1f2fb37-590a-4055-b21d-f5732c73d481"",
                ""Title"": ""Untitled Presentation"",
                ""ThumbnailUrl"": ""https://0701.static.prezi.com/preview/v2/ongheth6dj2n532tnynjh56wdx6jc3sachvcdoaizecfr3dnitcq_3_0.png"",
                ""Description"": ""This is our current best untitled prezi."",
                ""Privacy"": ""Hidden"",
                ""LastModified"": ""2017-01-29T19:26"",
                ""Owner"": {
                    ""FirstName"": ""Rezso"",
                    ""LastName"": ""Villany"",
                    ""Id"": ""3c9f159d-a4f7-4313-8318-23f39c2a79d3"",
                }
            }";

            // Act
            var presentation = JsonConvert.DeserializeObject<Presentation>(json);

            // Assert
            Assert.IsNotNull(presentation);
            Assert.That(presentation.Id, Is.EqualTo(Guid.Parse("c1f2fb37-590a-4055-b21d-f5732c73d481")));
            Assert.That(presentation.Title, Is.EqualTo("Untitled Presentation"));
            Assert.That(presentation.ThumbnailUrl, Is.EqualTo(new Uri("https://0701.static.prezi.com/preview/v2/ongheth6dj2n532tnynjh56wdx6jc3sachvcdoaizecfr3dnitcq_3_0.png")));
            Assert.That(presentation.Description, Is.EqualTo("This is our current best untitled prezi."));
            Assert.That(presentation.Privacy, Is.EqualTo("Hidden"));
            Assert.That(presentation.LastModified, Is.EqualTo(new DateTime(2017, 1, 29, 19, 26, 0)));
            Assert.IsNotNull(presentation.Owner);
            Assert.That(presentation.Owner.FirstName, Is.EqualTo("Rezso"));
            Assert.That(presentation.Owner.LastName, Is.EqualTo("Villany"));
            Assert.That(presentation.Owner.Id, Is.EqualTo(Guid.Parse("3c9f159d-a4f7-4313-8318-23f39c2a79d3")));


        }
    }
}
