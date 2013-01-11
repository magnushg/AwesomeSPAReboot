using AwesomeSPAReboot.Models;

namespace AwesomeSPAReboot.Services
{
    public interface ISearchRepository
    {
        void SaveSearch(Search search);
        Search GetById(string id);
        void SaveSearches(Search[] searches);
        Search[] GetAll();
    }
}