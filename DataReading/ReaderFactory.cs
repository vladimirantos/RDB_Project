using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB_Project.DataReading
{
    class ReaderFactory
    {
        private ISearching _searcher;

        private Paginator _paginator;

        public ReaderFactory(ISearching searcher, Paginator paginator)
        {
            _searcher = searcher;
            _paginator = paginator;
        }

        public IEnumerable<SearchResult> GetResults()
        {
            return _searcher.Search().Skip(_paginator.Offset).Take(_paginator.Length);
        }

        public static ReaderFactory CreateFactory(SearchResult searchArgument, int itemsPerPage)
        {
            ISearching searcher = new DatabaseReader(searchArgument);
            Paginator paginator = new Paginator(itemsPerPage, searcher.TotalRecords);
            return new ReaderFactory(searcher, paginator);
        }
    }
}
