using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    public interface IPresentationOnlineFetcher
    {
        Task<Presentations> TryGetOnlinePresentations();

        Task<Presentations> TryGetOnlinePresentationsAndSave();
    }
}