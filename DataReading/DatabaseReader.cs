using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDB_Project.Logging;

namespace RDB_Project.DataReading
{
    interface ISearching
    {
        List<SearchResult> Search();
    }
    class DatabaseReader : ISearching
    {
        private SearchResult _measurement;
        public DatabaseReader(SearchResult measurement)
        {
            _measurement = measurement;
        }

        public List<SearchResult> Search()
        {
            List<SearchResult> measurements = new List<SearchResult>();
            using (DbEntities entities = new DbEntities())
            {
                var query = from result in entities.SearchResults select result;
                return query.ToList();
            }
        }
    }
}
