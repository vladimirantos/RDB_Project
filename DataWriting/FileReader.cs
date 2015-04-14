using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RDB_Project.DataWriting
{
    interface IFileReader
    {
        void Read(BlockingCollection<string> output);
    }

    class FileReader : IFileReader
    {
        public FileReader(string path)
        {

        }

        public void Read(BlockingCollection<string> output)
        {
            throw new NotImplementedException();
        }
    }
}
