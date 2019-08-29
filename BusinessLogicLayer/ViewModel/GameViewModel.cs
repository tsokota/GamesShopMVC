using Model;
using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace BusinessLogicLayer.ViewModel
{
    public class GameViewModel
    {
        public Game Game { set; get; }

        public MultiSelectList MultiListGenres { set; get; }

        public MultiSelectList MultiListTypes { set; get; }

        public MultiSelectList MultiListPublisher { set; get; }

        [Required]
        public List<int> PlatformList { get; set; }


        public List<string> GenreList { get; set; }

        [Required]
        public List<string> PublisherList { get; set; }

        [DataType(DataType.MultilineText)]
        public string AdditionalLangauge { get; set; }

        public List<Language> Languages { get; set; }

        public string LanguageCode { get; set; }

        public GameViewModel(Game game, IEnumerable<SelectListItem> genres, IEnumerable<SelectListItem> platform,
            IEnumerable<SelectListItem> publisher, List<Language> languages)
        {
            Game = new Game();
            Game = game;
            MultiListGenres = new MultiSelectList(genres, "Value", "Text");
            MultiListTypes = new MultiSelectList(platform, "Value", "Text");
            MultiListPublisher = new MultiSelectList(publisher, "Value", "Text");
            PublisherList = new List<string>();
            PlatformList = new List<int>();
            GenreList = new List<string>();
            Languages = languages;

        }

        public GameViewModel()
        { }
    }
}
