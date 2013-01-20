using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AwesomeSPAReboot.Models;
using AwesomeSPAReboot.Services;

namespace AwesomeSPAReboot.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly IImagesService _imagesService;

        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        public IEnumerable<ImageData> Get()
        {
            return _imagesService.GetImages("cat").ToList();
        }

        // GET api/values/search
        public IEnumerable<ImageData> Get(string searchTerm)
        {
            return _imagesService.GetImages(searchTerm);
        }
    }
}