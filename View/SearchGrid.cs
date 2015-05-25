using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using RDB_Project.DataReading;

namespace RDB_Project.View
{
    class SearchGrid:AbstractGrid<SearchResult>
    {

        private SearchGrid(List<SearchResult> data)
            : base(data)
        {
            _data = data;
            _grid = new DataGrid();
            AddValues();
<<<<<<< HEAD
            ColNamesList = new List<string> { "IDBod", "IDMereni", "Datum", "x", "y", "Hodnota 1", "Hodnota 2", "Rozdíl Hodnot", "Přístroj", "Přesnost" };
=======
            ColNamesList = new List<string> {"IDBod", "IDMereni", "Datum","x","y","Hodnota 1","Hodnota 2","Rozdíl Hodnot","Přístroj","Přesnost"};
>>>>>>> 78a6233bb799a9c1eaed17247caa1af1cd508987
            _grid.AlternatingRowBackground = Brushes.DarkSeaGreen;
        }

        /// <summary>
        /// Vytvoří DataGrid ze vstupní kolekce Dblog
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataGrid CreateGrid(List<SearchResult> data)
        {
            return new SearchGrid(data).SetColumnNames().Grid;
        }
    }
}
