using BusinessLogicLayer.ViewModel.FilterModel;
using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel
{
    public class GamesViewModel
    {
        public IEnumerable<Game> Games
        { get; set; }

        public PaginationViewModel Pagination { get; set; }

        public FilterViewModel Filters { get; set; }

        public GamesViewModel()
        {
            Filters = new FilterViewModel();
            Pagination = new PaginationViewModel();
        }
    }
}
