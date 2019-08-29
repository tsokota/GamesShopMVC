using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.SiteComparer
{
    class GameComparer : IEqualityComparer<Game>
    {
        public bool Equals(Game x, Game y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Game obj)
        {
            return obj == null ? 0 : obj.Name.GetHashCode();
        }
    }
}
