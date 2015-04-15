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

    abstract class DatabaseWriter : IDatabaseWriter
    {
        public static void DeleteDatabase()
        {
            File.Delete("database.txt");
        }

        protected void Save(string str)
        {
            using (StreamWriter sw = new StreamWriter(@"database.txt", true))
            {
                sw.Write(str.ToString());
            }
        }

        protected string ToSql(string s)
        {
            return string.Format("VALUES({0})", s);
        }

        public abstract void Write(BlockingCollection<DatabaseObjects> input);
        public abstract void Write(BlockingCollection<string> input);
    }

    class BufferedDatabaseWriter : DatabaseWriter
    {

        StringBuilder str = new StringBuilder();

        int _bufferSize;

        public BufferedDatabaseWriter(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public override void Write(BlockingCollection<DatabaseObjects> input)
        {
            throw new NotImplementedException();
        }

        public override void Write(BlockingCollection<string> input)
        {
            int counter = 0;
            
            foreach (string s in input.GetConsumingEnumerable())
            {

                if (counter == _bufferSize)
                {
                   // Save(str.ToString());
                    counter = 0;
                    str = new StringBuilder();
                }
                str.AppendLine(ToSql(s));
                counter++;
            }

            //if (input.IsAddingCompleted)
            //    Save(str.ToString()); 
        }
    }
}
