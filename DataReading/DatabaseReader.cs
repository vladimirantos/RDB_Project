using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RDB_Project.Logging;

namespace RDB_Project.DataReading
{
    interface ISearching
    {
        int TotalRecords { get; }

        Task<IEnumerable<SearchResult>> SearchAsync();
    }

    class DatabaseReader : ISearching
    {
        private List<SearchResult> data = new List<SearchResult>();

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
        }

        /// <summary>
        /// Vrací seznam všech záznamů.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SearchResult>> SearchAsync()
        {
            _results = await Query();
            Log.Select("SearchResult", TotalRecords);
            return _results;
        }

        private async Task<IEnumerable<SearchResult>> Query() // --- zjistit limit pro zaznamy, vysledek davat do vlastnosti
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
                return await result.ToListAsync();
            }    
        }

        public List<SearchResult> SearchedResults
        {
            get { return data; }
        }

        public static Dictionary<int, string> GetMTypes()
        {
            using (var entities = new DbEntities())
            {
                var result = (from res in entities.MTypes select res).ToList();
                Dictionary<int, string> mTypes = new Dictionary<int, string>();
                foreach (MType mType in result)
                {
                    mTypes.Add(mType.idType, mType.name);
                }
                return mTypes;
            }
        }

        public static Dictionary<string, string> GetDevices()
        {
            using (var entities = new DbEntities())
            {
                var result = (from res in entities.Devices select res).ToList();
                Dictionary<string, string> devices = new Dictionary<string, string>();
                foreach (Device device in result)
                {
                    devices.Add(device.serialNumber, device.description);
                }
                return devices;
            }
        }

        public static int GetMaxIdMeasurement()
        {
            using (var entities = new DbEntities())
            {
                
                var firstOrDefault = entities.Measurements.OrderByDescending(m => m.idMeasurement).FirstOrDefault();
                if (firstOrDefault != null)
                    return firstOrDefault.idMeasurement;
                return 1;
            }
        }

        public static int DatabaseSize()
        {
            using (var entities = new DbEntities())
            {
                return (from c in entities.Measurements select c).Count();
            }
        }
    }
}
