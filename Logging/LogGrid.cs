using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Reflection;
using System.Windows.Media;

namespace RDB_Project.Logging
{
    internal class RdbException : ApplicationException
    {
        public RdbException(string message) : base(message)
        {
        }
    }

    class LogGrid:AbstractGrid<Dblog>
    {
        private readonly List<Dblog> _data;
        private readonly DataGrid _grid;
         
        private LogGrid(List<Dblog> data):base(data)
        {
            _data = data;
            _grid = new DataGrid();
            AddValues();
            ColNamesList = new List<string> {"Akce", "Tabulka", "Čas"};
            _grid.AlternatingRowBackground = Brushes.DarkSeaGreen;
        }


        /// <summary>
        /// Nastavení sloupců DataGridu
        /// </summary>
        public override AbstractGrid<Dblog> SetColumnNames()
        {
            try
            {
                PropertyInfo[] names = _data[0].GetType().GetProperties();
                int count = 0;

                foreach (PropertyInfo name in names)
                {
                    if (name.Name == "id_log") continue;
                    DataGridTextColumn c = new DataGridTextColumn
                    {
                        Header = ColNamesList[count],
                        Binding = new Binding(name.Name)
                    };
                    _grid.Columns.Add(c);
                    count++;
                }
                return this;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new RdbException("Nejsou uloženy žádné data");
            }
        }

        /// <summary>
        /// Vytvoří DataGrid ze vstupní kolekce Dblog
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataGrid CreateGrid(List<Dblog> data)
        {
            return new LogGrid(data).SetColumnNames().Grid;
        }
    }
}
