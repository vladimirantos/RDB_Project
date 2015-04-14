using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RDB_Project.DataWriting
{
    interface IDatabaseWriter
    {
        void Write(BlockingCollection<DatabaseObjects> input);

        /// <summary>
        /// Testovací metoda
        /// </summary>
        /// <param name="input"></param>
        void Write(BlockingCollection<string> input);
    }

    class DatabaseWriter : IDatabaseWriter
    {
        public void Write(BlockingCollection<DatabaseObjects> input)
        {
            throw new NotImplementedException();
        }

        public void Write(BlockingCollection<string> input)
        {
            throw new NotImplementedException();
        }
    }
}
