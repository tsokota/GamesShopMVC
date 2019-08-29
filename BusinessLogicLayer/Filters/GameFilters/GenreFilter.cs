using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{

    public class GenreFilter : FilterBase<IQueryable<Game>>
    {
        private readonly IEnumerable<int> _genreFilterId;

        public GenreFilter(IEnumerable<int> genreFilterId)
        {
            _genreFilterId = genreFilterId;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            return games.Where(a => a.Genres.Any(x => _genreFilterId.Contains(x.Id)));
        }
    }

}
