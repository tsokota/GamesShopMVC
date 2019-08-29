using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Model.Filtering
{
    public class FilterArgs
    {
        public IEnumerable<int> SelectedGenres { get; set; }

        public IEnumerable<Genre> AvailableGenres { get; set; }

        public IEnumerable<int> SelectedPlatforms { get; set; }

        public IEnumerable<Platform> AvailablePlatforms { get; set; }

        public IEnumerable<int> SelectedPublishers { get; set; }

        public IEnumerable<Publisher> AvailablePublishers { get; set; }

        public PopularityType FilterType { get; set; }

        public DatesFilter SelectedDate { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string GameName { get; set; }

        public FilterArgs()
        {
            FilterType = PopularityType.ByPriceAsc;
        }
    }
}
