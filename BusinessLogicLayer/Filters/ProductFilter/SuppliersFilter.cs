using BusinessLogicLayer.Filters.PipelinePattern;
using DAL;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Filters.ProductFilter
{
    public class SupplierFilter : FilterBase<IQueryable<Product>>
    {
        private readonly IEnumerable<int> _filterSupplierName;

        public SupplierFilter(IEnumerable<int> supplierNames)
        {
            _filterSupplierName = supplierNames;
        }

        protected override IQueryable<Product> Process(IQueryable<Product> input)
        {
            var result = input;
            if (_filterSupplierName != null)
            {
                result = result.Where(a => a.SupplierID.HasValue && _filterSupplierName.Any(b => b == a.Supplier.SupplierID));
            }
            return result;
        }
    }
}
