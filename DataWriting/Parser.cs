using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RDB_Project.DataWriting
{
    interface IParser
    {
        void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output);

        void Parse(BlockingCollection<List<string>> input, BlockingCollection<List<string>> output);
    }

    class Parser : IParser
    {
        public void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output)
        {
            throw new NotImplementedException();
        }

        public void Parse(BlockingCollection<List<string>> input, BlockingCollection<List<string>> output)
        {
            //System.Windows.MessageBox.Show(input.Count.ToString());   
            try
            {
                foreach (List<string> item in input.GetConsumingEnumerable())
                {
                    //System.Windows.MessageBox.Show("Data parser: " + item);
                    output.Add(item);
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }
    }
}
