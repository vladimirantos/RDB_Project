﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Globalization;
using RDB_Project.DataReading;
using System.Windows;
using System.Windows.Shapes;

namespace RDB_Project.DataWriting
{
    interface IParser
    {
        void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output);
        Dictionary<int, string> ExistingMTypes { set; }
        Dictionary<string, string> ExistingDevices { set; }
        int IdMeasurement { set; }
    }

    class StringParser : IParser
    { 
        private static int _idMeasurement = 1;
        public int IdMeasurement { set { _idMeasurement = value + 1; } }
        private int _bufferSize;

        private Dictionary<int, string> _existingMTypes = new Dictionary<int, string>(); 
        private Dictionary<string, string> _existingDevices = new Dictionary<string, string>();
        public Dictionary<int, string> ExistingMTypes { set { _existingMTypes = value; } }
        public Dictionary<string, string> ExistingDevices { set { _existingDevices = value; } }
        public StringParser(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public void Parse(BlockingCollection<string> input, BlockingCollection<DatabaseObjects> output)
        {
            try
            {
                List<Device> devices = new List<Device>();
                List<Measurement> measurements = new List<Measurement>();
                List<MType> mTypes = new List<MType>();
                List<Point> points = new List<Point>();

              //  IEnumerator<string> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                DatabaseObjects databaseObjects = new DatabaseObjects();
                foreach (string s in input.GetConsumingEnumerable())
                {
                    string line = s;
                    string[] items = line.Split(';');

                    Device device = new Device();
                    MType mType = new MType();
                    Measurement measurement = new Measurement();
                    Point point = new Point();

                    device.serialNumber = items[9];

                    device.accuracy = float.Parse(items[10], CultureInfo.InvariantCulture);
                    device.description = items[11];

                    if (!_existingDevices.ContainsKey(device.serialNumber))
                    {
                        _existingDevices.Add(device.serialNumber, device.description);
                        devices.Add(device); 
                    }
                    
                    
                    mType.idType = int.Parse(items[12]);
                    mType.name = items[13];
                    if (!_existingMTypes.ContainsKey(mType.idType))
                    {
                        mTypes.Add(mType);
                        _existingMTypes.Add(mType.idType, mType.name);
                    }
                    
                    measurement.idMeasurement = _idMeasurement;
                    measurement.serialNumberDevice = device.serialNumber;
                    measurement.idMtype = mType.idType;
                    measurement.description = "";
                    measurement.unit = items[1];
                    measurement.date = timestampToDateTime(items[0]);
                    measurements.Add(measurement); 
                    
                    point.id_point = int.Parse(items[2]);
                    point.idMeasurement = measurement.idMeasurement;
                    point.id_point = measurement.idMeasurement;
                    point.x = Math.Round(float.Parse(items[3], CultureInfo.InvariantCulture), 2);
                    point.y = Math.Round(float.Parse(items[4], CultureInfo.InvariantCulture), 2);
                    point.value1 = float.Parse(items[6], CultureInfo.InvariantCulture);
                    point.value2 = float.Parse(items[7], CultureInfo.InvariantCulture);
                    point.variance = float.Parse(items[8], CultureInfo.InvariantCulture);
                    point.description = items[5];
                    points.Add(point);

                    if (_idMeasurement % _bufferSize == 0)
                    {
                        databaseObjects.Devices = new List<Device>(devices);
                        databaseObjects.MTypes = new List<MType>(mTypes);
                        databaseObjects.Measurements = new List<Measurement>(measurements);
                        databaseObjects.Points = new List<Point>(points);

                        devices = new List<Device>();
                        mTypes = new List<MType>();
                        measurements = new List<Measurement>();
                        points = new List<Point>();
                        output.Add(databaseObjects);
                        databaseObjects = new DatabaseObjects();
                    }
                    _idMeasurement++;
                }
                if (measurements.Count > 0)
                {
                    databaseObjects.Devices = new List<Device>(devices);
                    databaseObjects.MTypes = new List<MType>(mTypes);
                    databaseObjects.Measurements = new List<Measurement>(measurements);
                    databaseObjects.Points = new List<Point>(points);

                    
                    devices = new List<Device>();
                    mTypes = new List<MType>();
                    measurements = new List<Measurement>();
                    points = new List<Point>();
                    output.Add(databaseObjects);
                    databaseObjects = new DatabaseObjects();
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }

        private DateTime timestampToDateTime(string timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(double.Parse(timestamp)).ToLocalTime();
            return dtDateTime;
            // System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 1, 0);
            // return dateTime.AddSeconds(long.Parse(timestamp));
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

        public Dictionary<int, string> ExistingMTypes { set; private get; }
        public Dictionary<string, string> ExistingDevices { set; private get; }
        public int IdMeasurement { set; private get; }
    }
}
