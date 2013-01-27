using System.Collections.Generic;
using AwesomeSPAReboot.Models;

namespace AwesomeSPAReboot.Services
{
    public interface IImagesService
    {
        IEnumerable<ImageData> GetImages(string searchTerm, bool scheduled = false);
    }
}
