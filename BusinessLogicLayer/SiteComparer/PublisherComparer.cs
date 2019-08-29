using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.SiteComparer
{
    class PublisherComparer : IEqualityComparer<Publisher>
    {
        public bool Equals(Publisher x, Publisher y)
        {
            return x.CompanyName == y.CompanyName;
        }

        public int GetHashCode(Publisher obj)
        {
            return obj == null ? 0 : obj.CompanyName.GetHashCode();
        }
    }
}
