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
        private string _condition;
        private int _count;

        public LogActions Action { get { return _action; } }

        public string Table { get { return _table; } }

        public string Condition { get { return _condition; } }

        public int Count { get { return _count; } }

        public Log(LogActions action, string table, string condition, int count)
        {
            _action = action;
            _table = table;
            _condition = condition;
            _count = count;
        }

        public void Save()
        {
            Dblog dblog = new Dblog();
            
        }
    }
}
