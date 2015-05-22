using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB_Project.DataReading
{
    class Paginator
    {
        private int _itemsPerPage;

        private int _totalRecords;

        /// <summary>
        /// Aktuální stránka
        /// </summary>
        public int CurrentPage
        {
            get; set;
        }

        /// <summary>
        /// Celkový počet stran
        /// </summary>
        public double TotalPages
        {
            get{ return Math.Floor((double)_totalRecords / _itemsPerPage); }
        }

        /// <summary>
        /// Číslo prvního záznamu na stránce
        /// </summary>
        public int Offset
        {
            get
            {
                double offset = Length - (_itemsPerPage);
                if(offset > _totalRecords)
                    throw new IndexOutOfRangeException("Offset je za poslední stránkou.");
                return (int)offset;
            }
        }

        /// <summary>
        /// Číslo posledního záznamu na stránce
        /// </summary>
        public int Length
        {
            get
            {
                int length = _itemsPerPage * CurrentPage;
                //if(length > _totalRecords)
                //    throw new IndexOutOfRangeException("Délka je za poslední stránkou");
                return length;
            }
        }

        public bool IsLast
        {
            get { return CurrentPage == TotalPages; }
        }

        public bool IsFirst
        {
            get { return CurrentPage == 1; }
        }

        /// <param name="itemsPerPage">Počet záznamů na stránku</param>
        /// <param name="totalRecords">Celkový počet záznamů</param>
        public Paginator(int itemsPerPage, int totalRecords)
        {
            _itemsPerPage = itemsPerPage;
            _totalRecords = totalRecords;
            
        }
    }
}
