namespace PreziViewer.Models
{
    public class Presentation
    {
        public Guid Id { get; }
        public string Title { get; }
        public Uri ThumbnailUrl { get; }
        public string Description { get; }
        public string Privacy { get; } // TODO  - change to enum
        public DateTime LastModified { get; }
        public Owner Owner { get; }

    }
}