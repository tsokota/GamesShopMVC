using Model.Entities;
using System.Collections.Generic;
using Model;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.ViewModel
{
    public class UpdateGameModel
    {
        public Game Game { get; set; }

        public string GameKey { get; set; }

        public IEnumerable<Genre> AvailableGenres { get; set; }

        public IEnumerable<Platform> AvailablePlatforms { get; set; }

        public IEnumerable<Publisher> AvailablePublishers { get; set; }

        public IEnumerable<string> SelectedGenres { get; set; }

        public IEnumerable<string> SelectedPlatforms { get; set; }

        public string SelectedPublisher { get; set; }

        public string LangCode { get; set; }

        public List<Language> languages { get; set; }

        [DataType(DataType.MultilineText)]
        public string additionalDescription { get; set; }
    }
}
