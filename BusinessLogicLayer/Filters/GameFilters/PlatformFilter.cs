using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
    public class PlatformFilter : FilterBase<IQueryable<Game>>
    {
        private readonly IEnumerable<int> _filterPlatformId;

        public PlatformFilter(IEnumerable<int> listPlatformId)
        {
            _filterPlatformId = listPlatformId;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            return games.Where(x => x.Platforms.Any(y => _filterPlatformId.Contains(y.Id)));
        }
    }
}
