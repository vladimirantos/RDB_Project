using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB_Project.DataReading
{
    class SearchInput : SearchResult
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public DateTime DateTimeFrom
        {
            get
            {
                return new DateTime(); // spojení datumu a času od
            }
        }

        public DateTime DateTimeTo
        {
            get
            {
                return new DateTime(); //spojení datumu a času do
            }
        }
    }
}
