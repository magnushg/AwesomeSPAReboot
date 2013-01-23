using System.Linq;
using AwesomeSPAReboot.Models;
using AwesomeSPAReboot.Services;
using Raven.Client;
using Raven.Client.Document;

namespace AwesomeSPAReboot.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IDocumentSession _session;

        public SearchRepository(IDocumentSession session)
        {
            _session = session;
        }

        public void SaveSearch(Search search)
        {
            _session.Store(search);
            _session.SaveChanges();
        }

        public Search GetById(string id)
        {
            return _session.Load<Search>(id);
        }

        public void SaveSearches(Search[] searches)
        {

            searches.ToList().ForEach(_session.Store);
            _session.SaveChanges();
        }

        public Search[] GetAll()
        {
            return _session.Query<Search>().ToArray();
        }
    }
}
