using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Model;
using System;
using System.Linq;
using System.Web.Mvc;
using DAL;
using Yevhenii_KoliesnikTask1.Filters;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using BusinessLogicLayer.Filters.PipelinePattern;
using BusinessLogicLayer.Filters.GameFilters;
using BusinessLogicLayer.ViewModel.FilterModel;
using BusinessLogicLayer.Filters;
using NLog.Interface;




namespace Yevhenii_KoliesnikTask1.Controllers
{
    [TimeWorkOfActionFilter]
    [ExceptionControllerAttribute]
    public class GameController : BaseController
    {
        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial

        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformService _platformService;
        private readonly IPublisherService _publisherService;
        private readonly Pipeline<IQueryable<Game>> _filter;
        private readonly ILanguageService _languageService;
        private PaginationFilter _paginationaFilter;

        public GameController(IGameService gameServices,
            IGenreService genreServices, IPlatformService platformServices,
            IPublisherService publisherServices, ILanguageService languageServices, ILogger logger)
            : base(logger)
        {
            _gameService = gameServices;
            _platformService = platformServices;
            _genreService = genreServices;
            _publisherService = publisherServices;
            _languageService = languageServices;
            _filter = new Pipeline<IQueryable<Game>>();
        }

        /// <summary>
        /// Rendering view for field (Get URL: /games/new). 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpGet]
        [ActionName("new")]
        public ActionResult New()
        {


            GameViewModel model = new GameViewModel(new Game(), _genreService.GetAllItemsAsSelectListItems(),
            _platformService.GetAllItemsAsSelectListItems(), _publisherService.GetAllItemsAsSelectListItems(),
            _languageService.GetAllLanguages());
            return View(model);

        }

        /// <summary>
        ///  Manager can create game (POST URL: /games/new). 
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("new")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        [ValidateInput(false)]
        public ActionResult New(GameViewModel gameModel)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fill field correctly");
                return View(new GameViewModel(new Game(), _genreService.GetAllItemsAsSelectListItems(),
                    _platformService.GetAllItemsAsSelectListItems(), _publisherService.GetAllItemsAsSelectListItems(),
                    _languageService.GetAllLanguages()));
            }
            // inicialization by side value
            var game = _gameService.GetSideLinks(gameModel);
            _gameService.New(game, gameModel.LanguageCode, gameModel.AdditionalLangauge);
            return Redirect("~/en/games");
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Update(string gameKey)
        {

            Game game = _gameService.GetByKey(gameKey, CurrentLangCode);
            if (game == null)
            {
                throw new Exception("game with that key not found");
            }

            UpdateGameModel model = new UpdateGameModel();
            model.Game = game;
            model.SelectedGenres = game.Genres != null ? game.Genres.Select(a => a.Name).ToList() : null;
            model.SelectedPlatforms = game.Platforms != null ? game.Platforms.Select(a => a.Name).ToList() : null;
            model.SelectedPublisher = game.Publisher != null ? game.Publisher.CompanyName : null;
            model.AvailableGenres = _genreService.GetAllItems();
            model.AvailablePlatforms = _platformService.GetAllItems();
            model.AvailablePublishers = _publisherService.GetAllItems();
            model.languages = _languageService.GetAllLanguages();

            GameLang gameLang = _languageService.GetGameLocalizations(game.Id).FirstOrDefault();

            if (gameLang != null)
            {

                model.LangCode = gameLang.Language.Code;
                model.additionalDescription = gameLang.Description;
            }
            return View("Update", model);
        }

        /// <summary>
        ///  User can edit game (POST URL: /games/update)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateGameModel model)
        {
            var game = model.Game;
            game.Genres = null;
            game.Platforms = null;

            game.UnregisterPlatformFromGame();
            if (ModelState.IsValid)
            {
                _gameService.Update(game);
                _gameService.AddLocalizatedDescription(model.GameKey, model.LangCode, model.additionalDescription);
                if (model.SelectedGenres != null && model.SelectedGenres.Any())
                {
                    _genreService.SetForGame(model.Game.Key, model.SelectedGenres);
                }
                if (model.SelectedPlatforms != null && model.SelectedPlatforms.Any())
                {
                    _platformService.SetForGame(model.Game.Key, model.SelectedPlatforms);
                }
                if (!string.IsNullOrWhiteSpace(model.SelectedPublisher))
                {
                    _publisherService.SetForGame(model.Game.Key, model.SelectedPublisher);
                }
                return RedirectToAction("AllGames");
            }
            model.AvailableGenres = _genreService.GetAllItems();
            model.AvailablePlatforms = _platformService.GetAllItems();
            model.AvailablePublishers = _publisherService.GetAllItems();
            model.languages = _languageService.GetAllLanguages();
            return View(model);
        }


        [HttpGet]
        [ActionName("remove")]
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(string gamekey)
        {

            Game game = _gameService.GetByKey(gamekey, CurrentLangCode);
            return View("Delete", game);

        }

        /// <summary>
        /// User can delete game (POST URL: /games/remove)
        /// </summary>
        /// <param name="gamekey"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("remove")]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteGame(string gamekey)
        {

            Game g = _gameService.GetByKey(gamekey, CurrentLangCode);
            _gameService.Delete(g);
            return RedirectToRoute("Games", new { action = "AllGames" });

        }

        /// <summary>
        ///  User can get all games (GET URL: /games)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AllGames()
        {

            GamesViewModel model = new GamesViewModel();
            InitializeSorting(model.Filters);
            InitializePagination(model.Pagination);
            InitializeFilters(model.Filters);
            model.Games = _gameService.Get(_filter, CurrentLangCode);

            model.Filters.AvailableGenres = _genreService.GetAllItems();
            model.Filters.AvailablePlatforms = _platformService.GetAllItems();
            model.Filters.AvailablePublishers = _publisherService.GetAllItems();
            model.Filters.AvailableDates = new DatesFilter().GetFilterDates();
            model.Pagination.GamesCount = _paginationaFilter.GamesCount;

            return View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AllGames(GamesViewModel model)
        {
            InitializeFilters(model.Filters);
            InitializeSorting(model.Filters);
            InitializePagination(model.Pagination);
            model.Filters.AvailableGenres = _genreService.GetAllItems();
            model.Filters.AvailablePlatforms = _platformService.GetAllItems();
            model.Filters.AvailablePublishers = _publisherService.GetAllItems();
            model.Filters.AvailableDates = new DatesFilter().GetFilterDates();
            model.Games = _gameService.Get(_filter, CurrentLangCode);
            model.Pagination.GamesCount = _paginationaFilter.GamesCount;


            return View(model);

        }

        /// <summary>
        ///  User can get game details by key (GET URL: /game/{key})
        /// </summary>
        /// <param name="gamekey"></param>
        /// <returns></returns>
        [HttpGet]
        //[OutputCache(Duration = 60, VaryByParam = "None")]
        public ActionResult GameDetails(string gamekey)
        {

            var game = _gameService.GetByKey(gamekey, CurrentLangCode);
            return View("GameDetails", game);

        }

        /// <summary>
        ///  User can download game (just return any binary file as response) (GET URL: /game/{gamekey}/download)
        /// </summary>
        /// <param name="gamekey"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("downloads")]
        public ActionResult Downloads(string gamekey, string filename)
        {
            if (string.IsNullOrWhiteSpace(gamekey))
            {
                Logger.Error("Gamekey is invalid or null, GameController.cs");
                return new HttpNotFoundResult();
            }

            if (filename.IsNullOrWhiteSpace())
                filename = "text.txt";

            string path = AppDomain.CurrentDomain.BaseDirectory + filename;
            return File(path, "application/txt", filename);
        }

        /// <summary>
        ///  should add item to the busket.
        /// </summary>
        /// <param name="gamekey"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Buy(string gamekey)
        {
            return RedirectToAction("AddGameToBucket", "Order", _gameService.GetByKey(gamekey, CurrentLangCode));
        }


        public ActionResult DownloadPicture(string key)
        {
            return View(key);
        }



        [HttpPost]
        public ActionResult DownloadPicture(HttpPostedFileBase Filedata, string gameKey)
        {
            
            var game = _gameService.GetByKey(gameKey, CurrentLangCode);
            var fileName = game.Key + "." + Path.GetExtension(Filedata.FileName);
            var res = Upload(fileName, Filedata);

            if (res)
            {
                game.Picture = fileName;
               _gameService.Update(game);
            }
            return View("DownloadPicture");
        }



        [HttpPost]
        public async  Task<ViewResult> DownloadPictureAsync(HttpPostedFileBase Filedata, string gameKey)
        {

            var game = _gameService.GetByKey(gameKey, CurrentLangCode);
            var fileName = game.Key + "." + Path.GetExtension(Filedata.FileName);
            var res = await UploadAsync(fileName, Filedata);

            if (res)
            {
                game.Picture = fileName;
                _gameService.Update(game);
            }
            return View("DownloadPicture");
        }


        private async Task<bool> UploadAsync(string fileName, HttpPostedFileBase file)
        {
            return  Upload(fileName, file);
        }

        private bool Upload(string fileName, HttpPostedFileBase file)
        {
            var res = false;
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Content/Images/gamepicture"), fileName);
                file.SaveAs(path);
                res = true;
            }
            return res;
        }


        /// <summary>
        ///  On layout in top part of page show total numbers of games. Cache this data for 1 minute. 
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60)]
        public int GamesCount()
        {

            int gamesCount;
            if (HttpContext.Cache["GamesCount"] == null)
            {
                gamesCount = _gameService.GetGamesCount();
                HttpContext.Cache.Add("GamesCount", gamesCount, null, DateTime.Now.AddMinutes(1),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            }
            gamesCount = Convert.ToInt32(HttpContext.Cache["GamesCount"]);
            ViewBag.GamesCount = gamesCount;
            return gamesCount;


        }

        /// <summary>
        ///  for work with filters games
        /// </summary>
        /// <param name="model"></param>
        #region FilterGame
        private void InitializeFilters(FilterViewModel model)
        {
            if (model.SelectedGenres != null && model.SelectedGenres.Any())
            {
                _filter.Register(new GenreFilter(model.SelectedGenres));
            }
            if (model.SelectedPlatforms != null && model.SelectedPlatforms.Any())
            {
                _filter.Register(new PlatformFilter(model.SelectedPlatforms));
            }
            if (model.SelectedPublishers != null && model.SelectedPublishers.Any())
            {
                _filter.Register(new PublisherFilter(model.SelectedPublishers));
            }

            if (!model.MaxPrice.HasValue)
                model.MaxPrice = _gameService.GetAllItems().Max(x => x.Price);
            if (!model.MinPrice.HasValue)
                model.MinPrice = 0;
            _filter.Register(new PriceFilter(model.MinPrice.Value, model.MaxPrice.Value));

            if (model.GameName != null)
            {
                _filter.Register(new GameNameFilter(model.GameName));
            }
            if (model.SelectedDate != null)
            {
                _filter.Register(new DateFilter(model.SelectedDate));
            }
        }

        private void InitializeSorting(FilterViewModel model)
        {
            _filter.Register(new PopularityFilter(model.FilterType));
        }

        private void InitializePagination(PaginationViewModel model)
        {
            _paginationaFilter = new PaginationFilter(model.PageNumber, model.CountPerPage);
            _filter.Register(_paginationaFilter);
        }
        #endregion

    }

}

