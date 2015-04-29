using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using EntityFramework.BulkInsert.Extensions;

namespace RDB_Project.DataWriting
{
    interface IDatabaseWriter
    {
        void Write(BlockingCollection<DatabaseObjects> input);
    }

    class DatabaseWriter : IDatabaseWriter
    {
        public void Write(BlockingCollection<DatabaseObjects> input)
        {
            using (var ctx = new DbEntities())
            {
                IEnumerator<DatabaseObjects> enumerator = input.GetConsumingEnumerable().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DatabaseObjects data = enumerator.Current;
                    ctx.BulkInsert(data.Data1);
                    ctx.BulkInsert(data.Data2);
                    ctx.BulkInsert(data.Data3);
                }
                ctx.SaveChanges();
            }
        }
    }
}
