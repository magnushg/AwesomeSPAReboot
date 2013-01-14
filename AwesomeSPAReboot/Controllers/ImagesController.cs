using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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

        public IEnumerable<InstagramBasicData> Get()
        {
            return _imagesService.GetImagesFromTag("cat").ToList();
        }

        // GET api/values/search
        public IEnumerable<InstagramBasicData> Get(string searchTerm)
        {
            return _imagesService.GetImagesFromTag(searchTerm);
        }
    }
}