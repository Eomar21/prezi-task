using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    public interface IPresentationFetcher
    {
        Task<IEnumerable<Presentation>> GetPresentations();
    }
}