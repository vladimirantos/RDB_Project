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

    class LogGrid:DataGrid
    {
        private readonly List<Dblog> _data;
        private readonly DataGrid _grid;
        private readonly List<string> _colNamesList = new List<string>{"Akce","Tabulka","Čas"};
        private LogGrid(List<Dblog> data)
        {
            _data = data;
            _grid = new DataGrid();
            SetColumnNames();
            AddValues();
            _grid.AlternatingRowBackground = Brushes.DarkSeaGreen;
        }


        /// <summary>
        /// Nastavení sloupců DataGridu
        /// </summary>
        private void SetColumnNames()
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
                        Header = _colNamesList[count],
                        Binding = new Binding(name.Name)
                    };
                    _grid.Columns.Add(c);
                    count++;
                }
            }
            catch(ArgumentOutOfRangeException e)
            {
                throw new RdbException("Nejsou uloženy žádné data");
            }
        }

        /// <summary>
        /// Přidávání hodnot do DataGridu
        /// </summary>
        private void AddValues()
        {
            _grid.AutoGenerateColumns = true;
            foreach (Dblog item in _data)
            {
                _grid.Items.Add(item);
            }
        }

        /// <summary>
        /// Vrací DataGrid
        /// </summary>
        private DataGrid Grid
        {
            get { return _grid; }
        }

        /// <summary>
        /// Vytvoří DataGrid ze vstupní kolekce Dblog
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataGrid CreateGrid(List<Dblog> data)
        {
            return new LogGrid(data).Grid;
        }
    }
}
