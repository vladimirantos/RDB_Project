using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using EntityFramework.BulkInsert.Extensions;
using RDB_Project.Logging;

namespace RDB_Project.DataWriting
{
    interface IDatabaseWriter
    { 
        void Write(BlockingCollection<DatabaseObjects> input);
    }

    //todo přidat logování, čítač počtu uložení
    class DatabaseWriter : IDatabaseWriter
    {
        private static int _countDevices = 0;
        private static int _countMTypes = 0;
        private static int _countMeasurements = 0;
        private static int _countPoints = 0;

        public void Write(BlockingCollection<DatabaseObjects> input)
        {
            
            using (var ctx = new DbEntities()) 
            {
                foreach (DatabaseObjects data in input.GetConsumingEnumerable())
                {
                    ctx.BulkInsert(data.Devices);
                    ctx.BulkInsert(data.MTypes);
                    ctx.BulkInsert(data.Measurements);
                    ctx.BulkInsert(data.Points);

                    //logování
                    _countDevices += data.Devices.Count;
                    _countMTypes += data.MTypes.Count;
                    _countMeasurements += data.Measurements.Count;
                    _countPoints += data.Points.Count;
                }
                //IEnumerator<DatabaseObjects> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                //while (enumerator.MoveNext())
                //{
                //    DatabaseObjects data = enumerator.Current;
                //    foreach (Device device in data.Devices)
                //    {
                //        MessageBox.Show(device.serialNumber);
                //    }
                //    //    ctx.BulkInsert(data.Devices);
                //    //    ctx.BulkInsert(data.MTypes);
                //    //    ctx.BulkInsert(data.Measurements);
                //    //    ctx.BulkInsert(data.Points);

                
                
                //}
                ctx.SaveChanges();
                Log.Insert("Devices", _countDevices);
                Log.Insert("MTypes", _countMTypes);
                Log.Insert("Measurements", _countMeasurements);
                Log.Insert("Points", _countPoints);
            }
        }
    }
}
