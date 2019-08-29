using System.Linq;
using BusinessLogicLayer.Filters.PipelinePattern;
using DAL;

namespace BusinessLogicLayer.Filters.ProductFilter
{
    public class ProductNameFilter : FilterBase<IQueryable<Product>>
    {
        private string namePattern;

        public ProductNameFilter(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                namePattern = name.ToLower();
            }
        }

        protected override IQueryable<Product> Process(IQueryable<Product> input)
        {
            var result = input;
            if (namePattern != null)
            {
                result = result.Where(a => a.ProductName.ToLower().Contains(namePattern));
            }
            return result;
        }
    }
}