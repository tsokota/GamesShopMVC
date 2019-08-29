using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using Model.Filtering;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
    public class PopularityFilter : FilterBase<IQueryable<Game>>
    {
        private readonly PopularityType _filterType;

        public PopularityFilter(PopularityType filterType)
        {
            _filterType = filterType;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> input)
        {
            IQueryable<Game> result = input;
            switch (_filterType)
            {
                case PopularityType.ByPriceAsc:
                    result = result.OrderBy(y => y.Price);
                    break;
                case PopularityType.ByPriceDesc:
                    result = result.OrderByDescending(y => y.Price);
                    break;
                case PopularityType.Commented:
                    result = result.OrderByDescending(y => y.Comments.Count());
                    break;
                case PopularityType.New:
                    result = result.OrderByDescending(y => y.GameProduction);
                    break;
                case PopularityType.Popular:
                    //  result = result.OrderByDescending(y => y.GameVisits.Count());
                    break;
            }
            return result;
        }
    }
}
