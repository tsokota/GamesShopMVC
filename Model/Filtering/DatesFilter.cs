using System.Collections.Generic;

namespace Model.Filtering
{
    public class DatesFilter
    {
       protected List<string> Dates;

       public DatesFilter()
        {
            Dates = new List<string> {"Last week", "Last month", "Last year", "2 years", "5 years", "All time"};
        }

        public IEnumerable<string> GetFilterDates()
        {
            return Dates;
        }
    }
}
