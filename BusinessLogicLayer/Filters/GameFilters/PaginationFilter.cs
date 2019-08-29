using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{

    public class PaginationFilter : FilterBase<IQueryable<Game>>
    {
        private readonly int _pageNumber;
        private readonly int _countPerPage;

        public int GamesCount { get; set; }

        public PaginationFilter(int pageNumber, int countPerPage)
        {
            _pageNumber = pageNumber;
            _countPerPage = countPerPage != 0 ? countPerPage : 10;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            GamesCount = games.Count();
            return games.Skip((_pageNumber - 1) * _countPerPage).Take(_countPerPage);
        }
    }

}
