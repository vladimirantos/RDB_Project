using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB_Project.DataWriting
{
    class DataWriteFactory
    {
        IFileReader reader;

        public DataWriteFactory(IFileReader reader, IParser parser, IDatabaseWriter writer)
        {
            reader = new FileReader("cesta");
        }

        public static DataWriteFactory Create(string path)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {

        }
    }
}
