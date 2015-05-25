using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using RDB_Project.DataReading;

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

        private static string _path;

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
            _path = path;
            IParser parser = new StringParser(bufferSize);
            parser.ExistingDevices = DatabaseReader.GetDevices();
            parser.ExistingMTypes = DatabaseReader.GetMTypes();
            parser.IdMeasurement = DatabaseReader.GetMaxIdMeasurement();
            return new DataWriteFactory(
                new FileReader(path),
                parser, 
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
