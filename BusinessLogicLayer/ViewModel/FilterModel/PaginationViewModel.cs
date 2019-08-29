using System;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel.FilterModel
{
    public class PaginationViewModel
    {
        private int _countPerPage;

        private int _gamesCount;

        public int PageNumber { get; set; }

        public int CountPerPage
        {
            get
            {
                return _countPerPage;
            }
            set
            {
                _countPerPage = value;

                TotalPageCount = (int)Math.Ceiling((double)_gamesCount / _countPerPage);
            }
        }

        public int GamesCount
        {
            get
            {
                return _gamesCount;
            }
            set
            {
                _gamesCount = value;

                TotalPageCount = (int)Math.Ceiling((double)_gamesCount / _countPerPage);
            }
        }

        public Dictionary<String, int> ItemsCount { get; set; }

        public int TotalPageCount { get; private set; }

        public PaginationViewModel()
        {
            PageNumber = 1;
            CountPerPage = 10;

            ItemsCount = new Dictionary<String, int>();

            ItemsCount.Add("10", 10);
            ItemsCount.Add("20", 20);
            ItemsCount.Add("50", 50);
            ItemsCount.Add("All", Int32.MaxValue);
        }
    }
}
