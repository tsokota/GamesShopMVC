using BusinessLogicLayer.Filters.GameFilters;
using Model.Entities;
using Model.Filtering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLogicLayer.ViewModel.FilterModel
{
    public class FilterViewModel
    {
        public IEnumerable<int> SelectedGenres { get; set; }

        public IEnumerable<Genre> AvailableGenres { get; set; }

        public IEnumerable<int> SelectedPlatforms { get; set; }

        public IEnumerable<Platform> AvailablePlatforms { get; set; }

        public IEnumerable<int> SelectedPublishers { get; set; }

        public IEnumerable<Publisher> AvailablePublishers { get; set; }

        public PopularityType FilterType { get; set; }

        public IEnumerable<string> AvailableDates { get; set; }

        public string SelectedDate { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string GameName { get; set; }

        public bool IsChanged { get; set; }

    }
}
