using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace RDB_Project.DataWriting
{
    class DataExport
    {
        private IEnumerable<SearchResult> _list;
        private DataExport(IEnumerable<SearchResult> list)
        {
            _list = list;
        }

        private void Export(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter soubor = new StreamWriter(fs);

            StringBuilder b;
            foreach (SearchResult radky in _list)
            {
                PropertyInfo[] names = radky.GetType().GetProperties();
                b = new StringBuilder();
                int count = 0;
                int maxCount = names.Length;
                foreach (PropertyInfo obj in names)
                {
                    string vysledek;

                    if (obj.PropertyType.ToString() == "System.DateTime")
                    {
                        DateTime t = (DateTime)(radky.GetType().GetProperty(obj.Name).GetValue(radky, null));
                        vysledek = (t - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
                    }
                    else
                    {
                         vysledek = radky.GetType().GetProperty(obj.Name).GetValue(radky, null).ToString();
                    }
                    b.Append(vysledek);
                    if (count < maxCount - 1)
                        b.Append(";");
                    count++;
                }
                soubor.WriteLine(b.ToString());
                b.Clear();
            }
            soubor.Close();
        }

        /// <summary>
        /// Uložení exportu do zvolené složky
        /// </summary>
        /// <param name="list">Data k uložení</param>
        /// <param name="path">Cesta kam uložit</param>
        public static void Save(IEnumerable<SearchResult> list, string path)
        {
            new DataExport(list).Export(path);
        }
    }
}
