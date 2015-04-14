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

        public int BufferSize
        {
            get;
            set;
        }

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
            return new DataWriteFactory(new FileReader(path), new Parser(), new DatabaseWriter()) { BufferSize = bufferSize};
        }

        public void Save()
        {
            BlockingCollection<string> lineBuffer = 
                new BlockingCollection<string>(new ConcurrentQueue<string>(), BufferSize);

            BlockingCollection<DatabaseObjects> objectBuffer = 
                new BlockingCollection<DatabaseObjects>(new ConcurrentQueue<DatabaseObjects>(), BufferSize);

            //Dočasný objekt, použije se objectBuffer
            BlockingCollection<string> parserBuffer = 
                new BlockingCollection<string>(new ConcurrentQueue<string>(), BufferSize); 

            var fileRead = _taskFactory.StartNew(() => _fileReader.Read(lineBuffer));
            //var fileParser = _taskFactory.StartNew(() => _parser.Parse(lineBuffer, objectBuffer));
            var fileParser = _taskFactory.StartNew(() => _parser.Parse(lineBuffer, parserBuffer));
            //var database = _taskFactory.StartNew(() => _writer.Write(objectBuffer));
            var database = _taskFactory.StartNew(() => _writer.Write(parserBuffer));
            Task.WaitAll(fileRead, fileParser, database);
        }
    }
}
