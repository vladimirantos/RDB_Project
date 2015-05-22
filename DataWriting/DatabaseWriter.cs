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

namespace RDB_Project.DataWriting
{
    interface IDatabaseWriter
    {
        void Write(BlockingCollection<DatabaseObjects> input);
    }

    //todo přidat logování, čítač počtu uložení
    class DatabaseWriter : IDatabaseWriter
    {
        public void Write(BlockingCollection<DatabaseObjects> input)
        {
           // MessageBox.Show(input.GetConsumingEnumerable().Count().ToString());
            using (var ctx = new DbEntities())
            {
                foreach (DatabaseObjects data in input.GetConsumingEnumerable())
                {
                    foreach (Device device in data.Devices)
                    {
                        MessageBox.Show("Writer: " + device);
                    }
                    ctx.BulkInsert(data.Devices);
                    ctx.BulkInsert(data.MTypes);
                    ctx.BulkInsert(data.Measurements);
                    ctx.BulkInsert(data.Points);
                    
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
            }
        }
    }
}
