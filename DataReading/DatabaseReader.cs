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

        List<SearchResult> Search();
    }

    class DatabaseReader : ISearching
    {

        private SearchResult _arguments;

        private IQueryable<SearchResult> _results;

        public int TotalRecords
        {
            get { return _results.Count(); }
        }

        /// <param name="arguments">Hodnoty podle kterých se bude vyhledávat</param>
        public DatabaseReader(SearchResult arguments)
        {
            _arguments = arguments;
            _results = Query();
        }

        /// <summary>
        /// Vrací seznam všech záznamů.
        /// </summary>
        /// <returns></returns>
        public List<SearchResult> Search() {  return _results.ToList(); }

        private IQueryable<SearchResult> Query() // --- vypis zadanych boxu z databaze, zjistit limit pro zaznamy, data strkat do searchResult, vysledek davat do vlastnosti
        {
            using (DbEntities entities = new DbEntities())
            {
                var r =
                    entities.SearchResults.Where(
                        x => x.dateFrom == _arguments.dateFrom && x.difference == _arguments.difference &&
                             x.serialNumber == _arguments.serialNumber && x.x == _arguments.x && x.y == _arguments.y).Take(2);
                return r;
                //return from result in entities.SearchResults select result;
            }    
        }
    }
}
