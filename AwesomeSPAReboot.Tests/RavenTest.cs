using System.Collections.Generic;
using System.Linq;
using AwesomeSPAReboot.Infrastructure;
using AwesomeSPAReboot.Models;
using NUnit.Framework;

namespace AwesomeSPAReboot.Tests
{
    [TestFixture]
    public class RavenTest
    {
        [Test]
        public void CanInitializeRaven()
        {
            var searchRepository = new SearchRepository();
        }

        [Test]
        public void CanSaveToRaven()
        {
            var searchRepository = new SearchRepository();
            var search = new Search
                             {
                                 Term = "Bouvet",
                             };
            searchRepository.SaveSearch(search);
        }

        [Test]
        public void CanRetrieveDataFromRaven()
        {
            var searchRepository = new SearchRepository();
            var result = searchRepository.GetById("searches/1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Term, Is.EqualTo("Hello"));
        }

        [Test]
        public void CanInsertMultipleSearches()
        {
            var searchRepository = new SearchRepository();
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
            searchRepository.SaveSearches(searches.ToArray());
        }

        [Test]
        public void CanRetrieveMultipleSearches()
        {
            var searchRepository = new SearchRepository();
            var result = searchRepository.GetAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}
