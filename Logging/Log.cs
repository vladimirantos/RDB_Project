using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDB_Project.DataReading;

namespace RDB_Project.Logging
{
    enum LogActions
    {
        Insert, Select, Update, Delete
    }
    class Log
    {
        private LogActions _action;
        private string _table;


        /// <summary>
        /// Vytvoří Log bez podmínky, použíje se pro akce Insert, Update a Delete
        /// </summary>
        private Log(LogActions action, string table)
        {
            _action = action;
            _table = table;
        }

        /// <summary>
        /// Provede uložení do databáze
        /// </summary>
        private void Save()
        {
            Dblog dblog = new Dblog
            {
                action = _action.ToString(),
                table = _table
            };
            using (var entities = new DbEntities())
            {
                entities.Dblogs.Add(dblog);
                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Vrací objekty Dblog
        /// </summary>
        /// <returns></returns>
        public static List<Dblog> GetData()
        {
            List<Dblog> data;
            using (var entities = new DbEntities())
            {                
                data = entities.Dblogs.ToList();
            }
            return data;
        }

        /// <summary>
        /// Uloží akci Update do logu
        /// </summary>
        public static void Update(string table)
        {
            new Log(LogActions.Update, table).Save();
        }

        /// <summary>
        /// Uloží akci Delete do logu
        /// </summary>
        public static void Delete(string table)
        {
            new Log(LogActions.Delete, table).Save();
        }

        /// <summary>
        /// Uloží akci Select do logu
        /// </summary>
        public static void Select(string table)
        {
            new Log(LogActions.Select, table).Save();
        }

        /// <summary>
        /// Uloží akci Insert do logu bez podmínky
        /// </summary>
        public static void Insert(string table)
        {
            new Log(LogActions.Insert, table).Save();
        }
    }
}
