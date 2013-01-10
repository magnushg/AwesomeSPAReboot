using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AwesomeSPAReboot.Services;

namespace AwesomeSPAReboot.Controllers
{
    public class ImagesController : ApiController
    {
        private IInstagramService _imagesService;

        public ImagesController(IInstagramService imagesService)
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