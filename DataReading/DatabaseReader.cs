using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RDB_Project.Logging;

namespace RDB_Project.DataReading
{
    interface ISearching
    {
        int TotalRecords { get; }

        IEnumerable<SearchResult> Search();
    }

    class DatabaseReader : ISearching
    {
        private List<SearchResult> data;

        private SearchInput _arguments;

        private IEnumerable<SearchResult> _results;

        public int TotalRecords
        {
            get { return _results.Count(); }
        }

        /// <param name="arguments">Hodnoty podle kterých se bude vyhledávat</param>
        public DatabaseReader(SearchInput arguments)
        {
            _arguments = arguments;
            _results = Query();
        }

        /// <summary>
        /// Vrací seznam všech záznamů.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SearchResult> Search()
        {
            return _results;
        }

        private IEnumerable<SearchResult> Query() // --- zjistit limit pro zaznamy, vysledek davat do vlastnosti
        {
            using (var entities = new DbEntities())
            {
                DateTime timeFrom = _arguments.DateTimeFrom;
                DateTime timeTo = _arguments.DateTimeTo;
                SearchResult searchResult = _arguments as SearchResult;
                
                var result =
                    from res in entities.SearchResults select res;
                if(timeFrom != new DateTime(1,1,1) || timeTo != new DateTime(1,1,1))
                result = result.Where(x => x.date >= timeFrom && x.date <= timeTo);
                if (searchResult.difference > -1)
                    result = result.Where(x => x.difference == searchResult.difference);
                if (searchResult.serialNumber != "")
                    result = result.Where(x => x.serialNumber == searchResult.serialNumber);
                if (searchResult.x > -1)
                    result = result.Where(x => x.x == searchResult.x);
                if (searchResult.y > -1)
                    result = result.Where(x => x.y == searchResult.y);

                data = result.ToList();
                return data;
            }    
        }

        public List<SearchResult> SearchedResults
        {
            get { return data; }
        }
    }
}
