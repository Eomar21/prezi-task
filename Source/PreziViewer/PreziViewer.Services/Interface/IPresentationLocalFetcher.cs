using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    internal interface IPresentationLocalFetcher
    {
        Task<Presentations> TryGetLocalPresentations();
    }
}