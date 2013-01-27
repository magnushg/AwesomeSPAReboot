using System.Collections.Generic;
using System.Linq;
using AwesomeSPAReboot.Infrastructure;
using AwesomeSPAReboot.Models;
using NUnit.Framework;
using Raven.Client.Document;

namespace AwesomeSPAReboot.Tests
{
    [TestFixture]
    public class RavenTest
    {
        private SearchRepository _searchRepository;
 
        [SetUp]
        public void Setup()
        {
             _searchRepository = new SearchRepository(new DocumentStore{ConnectionStringName = "RavenDb"}.Initialize());
        }

        [Test]
        public void CanSaveToRaven()
        {
            var search = new Search
                             {
                                 Term = "Bouvet",
                             };
            _searchRepository.SaveSearch(search);
        }

        [Test]
        public void CanInsertMultipleSearches()
        {
            var searches = new List<Search>
                               {
                                   new Search
                                       {
                                           Term = "Hello"
                                       },
                                   new Search
                                       {
                                           Term = "cat"
                                       }

                               };
            _searchRepository.SaveSearches(searches.ToArray());
        }

        [Test]
        public void CanRetrieveMultipleSearches()
        {
            var result = _searchRepository.GetAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}
