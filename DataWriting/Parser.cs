using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Globalization;
using RDB_Project.DataReading;

namespace RDB_Project.DataWriting
{
    interface IParser
    {
        void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output);
    }

    class StringParser : IParser
    {
        private List<Device> _devices = new List<Device>();
        private List<Measurement> _measurements = new List<Measurement>();
        List<MType> _mTypes = new List<MType>();
        private List<Point> _points = new List<Point>();
        private static int _idMeasurement = 1;
        private int _bufferSize;

        public StringParser(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output)
        {
            try
            {
                IEnumerator<string> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                DatabaseObjects databaseObjects = new DatabaseObjects();
                while (enumerator.MoveNext())
                {
                    string line = enumerator.Current;
                    string[] items = line.Split(';');

                    Device device = new Device();
                    Measurement measurement = new Measurement();
                    MType mType = new MType();
                    Point point = new Point();

                    device.serialNumber = items[9];
                    device.description = items[10];

                    mType.idType = int.Parse(items[11]);
                    mType.name = items[12];

                    measurement.idMeasurement = _idMeasurement;
                    measurement.serialNumberDevice = device.serialNumber;
                    measurement.idMtype = mType.idType;
                    measurement.description = "";
                    measurement.unit = items[1];
                    measurement.date = int.Parse(items[0]);

                    point.id_point = int.Parse(items[2]);
<<<<<<< HEAD
                    point.idMeasurement = measurement.idMeasurement;
=======
                    point.id_point = measurement.idMeasurement;
>>>>>>> b1419d96909f898cc2d2f557dda09b2de6ed0afe
                    point.x = float.Parse(items[3], CultureInfo.InvariantCulture);
                    point.y = float.Parse(items[4], CultureInfo.InvariantCulture);
                    point.value1 = float.Parse(items[6], CultureInfo.InvariantCulture);
                    point.value2 = float.Parse(items[7], CultureInfo.InvariantCulture);
                    point.variance = float.Parse(items[8], CultureInfo.InvariantCulture);
                    point.description = items[5];

                    _devices.Add(device);
                    _measurements.Add(measurement);
                    _mTypes.Add(mType);
                    _points.Add(point);
                    
                    if ((_devices.Count % _bufferSize) == 0) //vyprázdnění
                    {
                        databaseObjects.Devices = new List<Device>(_devices);
                        databaseObjects.Measurements = new List<Measurement>(_measurements);
                        databaseObjects.MTypes = new List<MType>(_mTypes);
                        databaseObjects.Points = new List<Point>(_points);
                        output.Add(databaseObjects);

                        _devices = new List<Device>();
                        _measurements = new List<Measurement>();
                        _mTypes = new List<MType>();
                        _points = new List<Point>();
                    }
                    if (_devices.Count > 0)
                    {
                        databaseObjects.Devices = new List<Device>(_devices);
                        databaseObjects.Measurements = new List<Measurement>(_measurements);
                        databaseObjects.MTypes = new List<MType>(_mTypes);
                        databaseObjects.Points = new List<Point>(_points);
                        output.Add(databaseObjects);

                        _devices = new List<Device>();
                        _measurements = new List<Measurement>();
                        _mTypes = new List<MType>();
                        _points = new List<Point>();
                    }
                    _idMeasurement++;
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }
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
            /*try
            {
                List<data1> ld1 = new List<data1>();
                List<data2> ld2 = new List<data2>();
                List<data3> ld3 = new List<data3>();

                IEnumerator<string> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                DatabaseObjects databaseObjects = new DatabaseObjects();
                while (enumerator.MoveNext())
                {
                    string rez = enumerator.Current;
                    string[] items = rez.Split(';');
                    data1 d1 = new data1();
                    data2 d2 = new data2();
                    data3 d3 = new data3();

                    d1.value1 = Int32.Parse(items[0]);
                    d1.value2 = Int32.Parse(items[1]);
                    ld1.Add(d1);
                    d2.value3 = Int32.Parse(items[2]);
                    d2.value4 = Int32.Parse(items[3]);
                    ld2.Add(d2);
                    d3.value5 = Int32.Parse(items[4]);
                    d3.value6 = Int32.Parse(items[5]);
                    ld3.Add(d3);
                    if ((ld1.Count % _bufferSize) == 0)
                    {
                        databaseObjects.Data1 = new List<data1>(ld1);
                        databaseObjects.Data2 = new List<data2>(ld2);
                        databaseObjects.Data3 = new List<data3>(ld3);
                        ld1 = new List<data1>();
                        ld2 = new List<data2>();
                        ld3 = new List<data3>();
                        output.Add(databaseObjects);
                    }
                }
                if(ld1.Count > 0)
                {
                    databaseObjects.Data1 = new List<data1>(ld1);
                    databaseObjects.Data2 = new List<data2>(ld2);
                    databaseObjects.Data3 = new List<data3>(ld3);
                    ld1 = new List<data1>();
                    ld2 = new List<data2>();
                    ld3 = new List<data3>();
                    output.Add(databaseObjects);
                }


            }
            finally
            {
                output.CompleteAdding();
            }*/
        }
    }
}
