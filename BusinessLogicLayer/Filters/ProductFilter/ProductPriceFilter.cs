using BusinessLogicLayer.Filters.PipelinePattern;
using DAL;
using System.Linq;


namespace BusinessLogicLayer.Filters.ProductFilter
{
   
        public class ProductPriceRangeFilter : FilterBase<IQueryable<Product>>
        {
            private decimal? _minPrice;
            private decimal? _maxPrice;

            public ProductPriceRangeFilter(decimal? minPrice, decimal? maxPrice)
            {
                _minPrice = minPrice;
                _maxPrice = maxPrice;
            }

            protected override IQueryable<Product> Process(IQueryable<Product> input)
            {
                var result = input;
                if (_minPrice.HasValue)
                {
                    result = result.Where(a => a.UnitPrice >= _minPrice);
                }
                if (_maxPrice.HasValue)
                {
                    result = result.Where(a => a.UnitPrice <= _maxPrice);
                }
                return result;
            }
       
    }
}
