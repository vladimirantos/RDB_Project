using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB_Project.DataWriting
{
    class DataWriteFactory
    {
        IFileReader _reader;

        IParser _parser;

        IDatabaseWriter _writer;

        public DataWriteFactory(IFileReader reader, IParser parser, IDatabaseWriter writer)
        {
            _reader = reader;
            _parser = parser;
            _writer = writer;
        }

        public static DataWriteFactory Create(string path)
        {
            return new DataWriteFactory(new FileReader(path), new Parser(), new DatabaseWriter());
        }

        public void Save()
        {

        }
    }
}
