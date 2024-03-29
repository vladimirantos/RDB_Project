﻿using System;
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

        public List<SearchResult> Results { get; private set; }

        public bool IsLastPage {  get { return _paginator.IsLast; } }

        public bool IsFirstPage { get { return _paginator.IsFirst; } }

        public int TotalRecords { get { return Results.Count(); } }

        public Paginator Paginator { get { return _paginator; } }

        public ReaderFactory(ISearching searcher, Paginator paginator)
        {
            _searcher = searcher;
            _paginator = paginator;
            _paginator.CurrentPage = 1;
        }

        public void NextPage()
        {
            _paginator.CurrentPage++;
        }

        public void PreviousPage()
        {
            _paginator.CurrentPage--;
        }

        public async Task<List<SearchResult>> Prepare()
        {
            IEnumerable<SearchResult> data = await _searcher.SearchAsync();
            Results = data.ToList();
            _paginator.TotalRecords = _searcher.TotalRecords;
            return Results;
        }

        public IEnumerable<SearchResult> GetResults()
        {
            return Results.Skip(_paginator.Length).Take(MainWindow.ItemsPerPage);
        }

        public void Remove(SearchResult searchResult)
        {
            Results.Remove(searchResult);
        }

        public static ReaderFactory CreateFactory(SearchInput searchArgument, int itemsPerPage)
        {
            ISearching searcher = new DatabaseReader(searchArgument);
            Paginator paginator = new Paginator(itemsPerPage);
            return new ReaderFactory(searcher, paginator);
        }
    }
}
