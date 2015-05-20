using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

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
            ColNamesList = new List<string> {"Datum","x","y","Hodnota 1","Hodnota 2","Rozdíl Hodnot","Přístroj","Přesnost"};
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
