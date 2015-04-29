using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RDB_Project.DataWriting
{
    class DataWriteFactory
    {
        IFileReader _fileReader;

        IParser _parser;

        IDatabaseWriter _writer;

        TaskFactory _taskFactory;

        public int BufferSize{ get; set; }

        public const int DefaultBufferSize = 100000;

        public DataWriteFactory(IFileReader reader, IParser parser, IDatabaseWriter writer)
        {
            _fileReader = reader;
            _parser = parser;
            _writer = writer;
            _taskFactory =  new TaskFactory(TaskCreationOptions.LongRunning, 
                                                    TaskContinuationOptions.None);
        }

        public static DataWriteFactory Create(string path, int bufferSize)
        {
            return new DataWriteFactory(
                new FileReader(path), 
                new Parser(bufferSize), 
                new DatabaseWriter()) { BufferSize = bufferSize};
        }

        public static DataWriteFactory Create(string path)
        {
            return Create(path, DefaultBufferSize);
        }

        public void Save()
        {
            BlockingCollection<string> lineBuffer =
                new BlockingCollection<string>(BufferSize);

            BlockingCollection<DatabaseObjects> objectBuffer = 
                new BlockingCollection<DatabaseObjects>(BufferSize);

            var fileRead = _taskFactory.StartNew(() => _fileReader.Read(lineBuffer));
            var fileParser = _taskFactory.StartNew(() => _parser.Parse(lineBuffer, objectBuffer));
            //var fileParser = _taskFactory.StartNew(() => _parser.Parse(lineBuffer, objectBuffer));
            var database = _taskFactory.StartNew(() => _writer.Write(objectBuffer));
            //var database = _taskFactory.StartNew(() =>  _writer.Write(parserBuffer));

            Task.WaitAll(fileRead, fileParser, database);
        }
    }
}
