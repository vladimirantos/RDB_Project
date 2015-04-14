using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

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
            FileStream fs = new FileStream("database.csv",FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach(string data in input)
            {
                sw.WriteLine(data);
            }

            throw new NotImplementedException();
        }
    }
}
