using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, string> _condition = new Dictionary<string, string>();
        private int _count;

        /// <summary>
        /// Vytvoří Log se zadanou podmínkou, vhodné pro Select
        /// </summary>
        private Log(LogActions action, string table, Dictionary<string, string> condition, int count)
        {
            _action = action;
            _table = table;
            _condition = condition;
            _count = count;
        }

        /// <summary>
        /// Vytvoří Log bez podmínky, použíje se pro akce Insert, Update a Delete
        /// </summary>
        private Log(LogActions action, string table, int count)
        {
            _action = action;
            _table = table;
            _count = count;
        }

        /// <summary>
        /// Provede uložení do databáze
        /// </summary>
        private void Save()
        {
            Dblog dblog = new Dblog
            {
                action = _action.ToString(),
                table = _table,
                condition = _condition.Count > 0 ? CreateCondition() : null,
                count_rows = _count
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
            List<Dblog> data = new List<Dblog>();
            using (var entities = new DbEntities())
            {                
                data = entities.Dblogs.ToList();
            }
            return data;
        }

        /// <summary>
        /// Uloží akci Update do logu
        /// </summary>
        public static void Update(string table, Dictionary<string, string> condition, int count)
        {
            new Log(LogActions.Update, table, condition, count).Save();
        }

        /// <summary>
        /// Uloží akci Delete do logu
        /// </summary>
        public static void Delete(string table, Dictionary<string, string> condition, int count)
        {
            new Log(LogActions.Delete, table, condition, count).Save();
        }

        /// <summary>
        /// Uloží akci Select do logu
        /// </summary>
        public static void Select(string table, Dictionary<string, string> condition, int count)
        {
            new Log(LogActions.Select, table, condition, count).Save();
        }

        /// <summary>
        /// Uloží akci Insert do logu bez podmínky
        /// </summary>
        public static void Insert(string table, int count)
        {
            new Log(LogActions.Insert, table, count).Save();
        }

        /// <summary>
        /// Uloží akci Update do logu bez podmínky
        /// </summary>
        public static void Update(string table, int count)
        {
            new Log(LogActions.Update, table, count).Save();
        }

        /// <summary>
        /// Uloží akci Delete do logu bez podmínky
        /// </summary>
        public static void Delete(string table, int count)
        {
            new Log(LogActions.Delete, table, count).Save();
        }

        /// <summary>
        /// Uloží akci Select do logu bez podmínky
        /// </summary>
        public static void Select(string table, int count)
        {
            new Log(LogActions.Select, table, count).Save();
        }

        private string CreateCondition()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (KeyValuePair<string, string> keyValuePair in _condition)
            {
                sb.Append(i < _condition.Count
                    ? string.Format("{0} = {1}, ", keyValuePair.Key, keyValuePair.Value)
                    : string.Format("{0} = {1}", keyValuePair.Key, keyValuePair.Value));
                i++;
            }
            return sb.ToString();
        }
    }
}
