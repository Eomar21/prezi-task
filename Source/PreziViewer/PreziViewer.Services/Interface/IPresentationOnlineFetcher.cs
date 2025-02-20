using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    internal interface IPresentationOnlineFetcher
    {
        Task<Presentations> TryGetOnlinePresentations();

        Task<Presentations> TryGetOnlinePresentationsAndSave();
    }
}