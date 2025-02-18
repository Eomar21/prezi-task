using PreziViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreziViewer.Services.Interface
{
    public interface IPresentationLocalFetcher
    {
        IEnumerable<Presentation> TryGetOnlinePresentations();
    }
}
