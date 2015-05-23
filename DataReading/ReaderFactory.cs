using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RDB_Project.DataReading
{
    class ReaderFactory
    {
        private ISearching _searcher;

        private Paginator _paginator;

        public IEnumerable<SearchResult> Results { get; private set; }

        public bool IsLastPage {  get { return _paginator.IsLast; } }

        public bool IsFirstPage { get { return _paginator.IsFirst; } }

        public int TotalRecords { get { return Results.Count(); } }

        public ReaderFactory(ISearching searcher, Paginator paginator)
        {
            _searcher = searcher;
            _paginator = paginator;
            _paginator.CurrentPage = 1;
            Results = _searcher.Search();
        }

        public void NextPage()
        {
            _paginator.CurrentPage++;
        }

        public void PreviousPage()
        {
            _paginator.CurrentPage--;
        }

        public IEnumerable<SearchResult> GetResults()
        {
            return Results.Skip(_paginator.Offset).Take(_paginator.Length);
        }

        public static ReaderFactory CreateFactory(SearchInput searchArgument, int itemsPerPage)
        {
            ISearching searcher = new DatabaseReader(searchArgument);
            Paginator paginator = new Paginator(itemsPerPage, searcher.TotalRecords);
            return new ReaderFactory(searcher, paginator);
        }
    }
}
