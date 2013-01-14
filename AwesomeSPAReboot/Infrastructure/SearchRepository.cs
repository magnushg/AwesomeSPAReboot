using System.Linq;
using AwesomeSPAReboot.Models;
using AwesomeSPAReboot.Services;
using Raven.Client;
using Raven.Client.Document;

namespace AwesomeSPAReboot.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IDocumentStore _store;
        
        public SearchRepository()
        {
            _store = new DocumentStore {ConnectionStringName = "RavenDb"};
            _store.Initialize();
        }

        public void SaveSearch(Search search)
        {
            using(var session = _store.OpenSession())
            {
                 session.Store(search);
                 session.SaveChanges();
            }
        }

        public Search GetById(string id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<Search>(id);
            }
        }

        public void SaveSearches(Search[] searches)
        {
            using (var session = _store.OpenSession())
            {
                searches.ToList().ForEach(session.Store);
                session.SaveChanges();
            }

        }

        public Search[] GetAll()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Search>().ToArray();
            }
        }
    }
}
