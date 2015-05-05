using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RDB_Project
{
    abstract class AbstractGrid<T>:DataGrid
    {
        private readonly List<T> _data;
        private readonly DataGrid _grid;
        protected List<string> ColNamesList { get; set; }
        protected AbstractGrid(List<T> data)
        {
            _data = data;
            _grid = new DataGrid();
            AddValues();
            _grid.AlternatingRowBackground = Brushes.DarkSeaGreen;
        }
        
        /// <summary>
        /// Nastavení sloupců DataGridu
        /// </summary>
        public virtual AbstractGrid<T> SetColumnNames()
        {
            try
            {
                PropertyInfo[] names = _data[0].GetType().GetProperties();
                int count = 0;

                foreach (PropertyInfo name in names)
                {
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
        /// Přidávání hodnot do DataGridu
        /// </summary>
        protected void AddValues()
        {
            _grid.AutoGenerateColumns = true;
            foreach (T item in _data)
            {
                _grid.Items.Add(item);
            }
        }

        /// <summary>
        /// Vrací DataGrid
        /// </summary>
        public DataGrid Grid
        {
            get { return _grid; }
        }
    }
}
