using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace RDB_Project.DataWriting
{
    interface IParser
    {
        void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output);
    }

    class Parser : IParser
    {
        int _bufferSize;

        public Parser(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output)
        {
            try
            {
                //List<data1> ld1 = new List<data1>();
                //List<data2> ld2 = new List<data2>();
                //List<data3> ld3 = new List<data3>();

                //IEnumerator<string> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                //DatabaseObjects databaseObjects = new DatabaseObjects();
                //while (enumerator.MoveNext())
                //{
                //    string rez = enumerator.Current;
                //    string[] items = rez.Split(';');
                //    data1 d1 = new data1();
                //    data2 d2 = new data2();
                //    data3 d3 = new data3();

                //    d1.value1 = Int32.Parse(items[0]);
                //    d1.value2 = Int32.Parse(items[1]);
                //    ld1.Add(d1);
                //    d2.value3 = Int32.Parse(items[2]);
                //    d2.value4 = Int32.Parse(items[3]);
                //    ld2.Add(d2);
                //    d3.value5 = Int32.Parse(items[4]);
                //    d3.value6 = Int32.Parse(items[5]);
                //    ld3.Add(d3);
                //    if ((ld1.Count % _bufferSize) == 0)
                //    {
                //        databaseObjects.Data1 = new List<data1>(ld1);
                //        databaseObjects.Data2 = new List<data2>(ld2);
                //        databaseObjects.Data3 = new List<data3>(ld3);
                //        ld1 = new List<data1>();
                //        ld2 = new List<data2>();
                //        ld3 = new List<data3>();
                //        output.Add(databaseObjects);
                //    }
                //}
                //if(ld1.Count > 0)
                //{
                //    databaseObjects.Data1 = new List<data1>(ld1);
                //    databaseObjects.Data2 = new List<data2>(ld2);
                //    databaseObjects.Data3 = new List<data3>(ld3);
                //    ld1 = new List<data1>();
                //    ld2 = new List<data2>();
                //    ld3 = new List<data3>();
                //    output.Add(databaseObjects);
                //}
            }
            finally
            {
                output.CompleteAdding();
            }
        }
    }
}
