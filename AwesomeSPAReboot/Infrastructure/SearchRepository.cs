using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AwesomeSPAReboot.Models;
using AwesomeSPAReboot.Services;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Document.Async;

namespace AwesomeSPAReboot.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IDocumentStore _documentStore;

        public SearchRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void SaveSearch(Search search)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(search);
                session.SaveChanges();
            }
           
        }

        public Search GetById(string id)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Load<Search>(id);
            }
        }

        public void SaveSearches(Search[] searches)
        {

            using (var session = _documentStore.OpenSession())
            {
                searches.ToList().ForEach(session.Store);
                session.SaveChanges();
            }
        }

        public Search[] GetAll()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<Search>().ToArray();
            }
        }
    }
}
