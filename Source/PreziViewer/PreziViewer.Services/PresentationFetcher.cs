﻿using PreziViewer.Models;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services
{
    internal class PresentationFetcher : IPresentationFetcher
    {
        private readonly IPresentationLocalFetcher m_PresentationLocalFetcher;
        private readonly IPresentationOnlineFetcher m_PresentationOnlineFetcher;

        public PresentationFetcher(IPresentationLocalFetcher presentationLocalFetcher, IPresentationOnlineFetcher presentationOnlineFetcher)
        {
            m_PresentationLocalFetcher = presentationLocalFetcher;
            m_PresentationOnlineFetcher = presentationOnlineFetcher;
        }

        public event EventHandler<bool>? IsSourceOnline;

        public async Task<IEnumerable<Presentation>> GetPresentations()
        {
            var onlinePresentations = await m_PresentationOnlineFetcher.TryGetOnlinePresentationsAndSave();
            if (!onlinePresentations.List.Any())
            {
                var localPresentations = await m_PresentationLocalFetcher.TryGetLocalPresentations();
                IsSourceOnline?.Invoke(this, false);
                return localPresentations.List;
            }
            IsSourceOnline?.Invoke(this, true);
            return onlinePresentations.List;
        }
    }
}