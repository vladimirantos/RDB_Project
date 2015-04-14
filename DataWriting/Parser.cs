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
    }

    class Parser : IParser
    {
        public void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output)
        {
            throw new NotImplementedException();
        }
    }
}
