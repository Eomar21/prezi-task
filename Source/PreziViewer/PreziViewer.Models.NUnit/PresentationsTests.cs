using Newtonsoft.Json;
using PreziViewer.Models;

namespace PreziViewer.Tests
{
    [TestFixture]
    public class PresentationsTests
    {
        [Test]
        public void DefaultConstructor_ShouldReturnEmptyList()
        {
            // Arrange & Act
            var presentations = new Presentations();

            // Assert
            Assert.IsNotNull(presentations.List);
            Assert.IsEmpty(presentations.List);
        }

        [Test]
        public void ParameterizedConstructor_ShouldSetListCorrectly()
        {
            // Arrange
            var presentationList = new List<Presentation>
            {
                new Presentation { Title = "Presentation 1", Id= new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg")},
                new Presentation { Title = "Presentation 2", Id= new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg") }
            };

            // Act
            var presentations = new Presentations(presentationList);

            // Assert
            Assert.IsNotNull(presentations.List);
            Assert.That(presentations.List.Count(), Is.EqualTo(2));
            CollectionAssert.AreEqual(presentationList, presentations.List.ToList());
        }

        [Test]
        public void JsonSerialization_ShouldUseCorrectPropertyName()
        {
            // Arrange
            var presentationList = new List<Presentation>
            {
                new Presentation { Title = "Test Presentation", Id= new Guid(), ThumbnailUrl=new Uri("https://Prezi.com/thumbnail.jpg") }
            };
            var presentations = new Presentations(presentationList);

            // Act
            var json = JsonConvert.SerializeObject(presentations);

            // Assert: Verify that the JSON contains the "presentations" property and the test title.
            StringAssert.Contains("\"presentations\"", json);
            StringAssert.Contains("Test Presentation", json);
        }
    }
}