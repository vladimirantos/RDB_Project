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

        private int _currentPage;

        private int _totalRecords;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }

        public double TotalPages
        {
            get{ return Math.Floor((double)_totalRecords/_itemsPerPage); }
        }

        //public double Offset
        //{
        //    get
        //    {
                
        //    }
        //}

        //public double Length
        //{
        //    get
        //    {
                
        //    }
        //}

        public Paginator(int itemsPerPage, int totalRecords)
        {
            _itemsPerPage = itemsPerPage;
            _totalRecords = totalRecords;
            
        }


    }
}
