using BusinessLogicLayer.Filters.PipelinePattern;
using DAL;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Filters.ProductFilter
{
    public class CategoryFilter : FilterBase<IQueryable<Product>>
    {
        private readonly IEnumerable<int> _filtersCategoryNames;

        public CategoryFilter(IEnumerable<int> categoryNames)
        {
            _filtersCategoryNames = categoryNames;
        }

        protected override IQueryable<Product> Process(IQueryable<Product> input)
        {
            var result = input;
            if (_filtersCategoryNames != null)
            {
                result = result.Where(a => _filtersCategoryNames.Any(b => a.Category.CategoryID == b));
            }
            return result;
        }
    }
}
