using System.Web.Http;
using AwesomeSPAReboot.Services;

namespace AwesomeSPAReboot.Controllers
{
    public class SampleController : ApiController
    {
        private ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public string[] Get()
        {
            return _sampleService.Hello();
        }
    }
}