using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AwesomeSPAReboot.Services;

namespace AwesomeSPAReboot.Controllers
{
    public class SearchstatsController : ApiController
    {
        private readonly ISearchRepository _searchRepository;

        public SearchstatsController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        // GET api/searchstats
        public IEnumerable<dynamic> Get()
        {
            return _searchRepository.GetAll().GroupBy(s => s.Term.ToLower().Trim(new[]{'#'})).OrderByDescending(g => g.Count()).Select(g => new {term = g.Key, count = g.Count()});
        }

        // GET api/searchstats/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
