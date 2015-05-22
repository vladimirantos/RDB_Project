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

        private IEnumerable<SearchResult> Query() // --- vypis zadanych boxu z databaze, zjistit limit pro zaznamy, data strkat do searchResult, vysledek davat do vlastnosti
        {
            using (var entities = new DbEntities())
            {

                DateTime from = _arguments.DateTimeTo;
                DateTime to = _arguments.DateTimeTo;
                SearchResult searchResult = _arguments as SearchResult;

               /* var r =
                    entities.SearchResults.Where(
                        x => x.dateFrom == _arguments.dateFrom && x.difference == _arguments.difference &&
                             x.serialNumber == _arguments.serialNumber && x.x == _arguments.x && x.y == _arguments.y).Take(2);*/

                var r = from ur in entities.SearchResults
                        where ur.serialNumber == "2148DRFES-583"
                    select ur;
                return r.ToList();
                //return from result in entities.SearchResults select result;
            }    
        }
    }
}
