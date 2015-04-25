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
        void Write(BlockingCollection<List<string>> input);
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
                sw.WriteLine("*****************************************************");
            }
        }

        protected string ToSql(string s)
        {
            return string.Format("VALUES({0})", s);
        }

        protected string ToSql(List<string> data)
        {
            return data.Aggregate((current, next) => current + ", " + next);
        }

        public abstract void Write(BlockingCollection<DatabaseObjects> input);
        public abstract void Write(BlockingCollection<List<string>> input);
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

        public override void Write(BlockingCollection<List<string>> input)
        {
           /* int counter = 0;
            foreach (List<string> items in input.GetConsumingEnumerable())
            {

                if (counter == _bufferSize)
                {
                    Save(str.ToString());
                    counter = 0;
                    str = new StringBuilder();
                }
                str.AppendLine(ToSql(s));
                counter++;
                System.Windows.MessageBox.Show(items.Count.ToString());
                string s = ToSql(items);
                Save(s);
            }

            if (input.IsAddingCompleted)
                Save(str.ToString()); */
        }
    }
}
