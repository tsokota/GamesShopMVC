using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.SiteComparer
{
    internal class GenreComparer : IEqualityComparer<Genre>
    {
        public bool Equals(Genre x, Genre y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Genre obj)
        {
            return obj == null ? 0 : obj.Name.GetHashCode();
        }
    }
}
