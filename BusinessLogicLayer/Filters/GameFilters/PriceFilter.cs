using BusinessLogicLayer.Filters.PipelinePattern;
using Model.Entities;
using System.Linq;


namespace BusinessLogicLayer.Filters.GameFilters
{
    public class PriceFilter : FilterBase<IQueryable<Game>>
    {
        private readonly double _minPrice;
        private readonly double _maxPrice;

        public PriceFilter(double minPrice, double maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        protected override IQueryable<Game> Process(IQueryable<Game> games)
        {
            return games.Where(x => x.Price <= _maxPrice && x.Price >= _minPrice);
        }
    }
}
