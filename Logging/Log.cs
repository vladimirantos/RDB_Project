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
        private int count;


        public LogActions Action
        {
            get { return _action; }
        }

        public string Table
        {
            get { return _table; }
        }
    }
}
