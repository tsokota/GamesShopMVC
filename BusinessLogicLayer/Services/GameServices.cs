using BusinessLogicLayer.Filters.GameFilters;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.ViewModel;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model.Entities;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Filters.PipelinePattern;
using AutoMapper;
using BusinessLogicLayer.SiteComparer;
using Model.Filtering;
using NLog.Interface;
using BusinessLogicLayer.Filters.ProductFilter;




namespace BusinessLogicLayer.Services
{

    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IPlatformService _platformService;

        public GameService(IUnitOfWork unitOfWork, ILogger logger, IPublisherService publisherService, IGenreService genreService, IPlatformService platformService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _genreService = genreService;
            _publisherService = publisherService;
            _platformService = platformService;
        }

        public void New(Game game)
        {
            New(game, "en");
        }

        public void New(Game game, string langCode = "en", string additionalDescription = null)
        {
            try
            {
                if (game == null)
                {
                    _logger.Error("game is null, GameService.cs");
                    throw new ArgumentNullException("game");

                }
                if (langCode == null)
                {
                    _logger.Error("langCode is null, GameService.cs");
                    throw new ArgumentNullException("langCode");
                }
                _unitOfWork.GameRepository.Insert(game);
                _unitOfWork.Save();
                _logger.Debug("GameService.cs, result succsess - added new game: {0}, {1}", game.Name, game.Key);
                //for realization localization make this code
                if (langCode != "en")
                {
                    AddLocalizatedDescription(game.Key, langCode, additionalDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GameService.cs, some error:  New(Game game)" + "\n" + ex.Message);
                throw;
            }
        }

        public void AddLocalizatedDescription(string gameKey, string languageCode, string newDescription)
        {
            var gameFromRepo = _unitOfWork.GameRepository.Get().FirstOrDefault(x => x.Key == gameKey);
            var language = _unitOfWork.LanguageRepository.Get().FirstOrDefault(l => l.Code == languageCode);

            if (language == null || gameFromRepo == null)
                throw new ArgumentNullException("gameKey");

            GameLang gameLanguage = _unitOfWork.GameLang.Get(game => game.Id == gameFromRepo.Id && game.LanguageId == language.Id).FirstOrDefault();

            if (gameLanguage == null)
            {
                gameLanguage = new GameLang
                 {
                     Id = gameFromRepo.Id,
                     Description = newDescription,
                     LanguageId = language.Id
                 };
                _unitOfWork.GameLang.Insert(gameLanguage);
            }
            else
            {
                gameLanguage.Description = newDescription;
                _unitOfWork.GameLang.Update(gameLanguage);
            }

            _unitOfWork.Save();
        }

        public void Update(Game game)
        {
            if (game.Key.Contains(SettingsConst.Prefix))
            {
                _unitOfWork.GameRepository.Insert(game);
            }
            else
            {
                _unitOfWork.GameRepository.Update(game);
            }
            _unitOfWork.Save();
        }

        public bool Delete(Game game)
        {
            try
            {
                if (game == null)
                {
                    _logger.Error("null [game] parametr data, GameService.cs");
                    throw new ArgumentException("gamekey is null");
                }
                if (game.Key.Contains(SettingsConst.Prefix))
                {
                    var northwindGame = FindGameInNorthwindDb(game.Key);
                    Game newGame = Mapper.Map<Game>(northwindGame);
                    newGame.IsDeleted = true;
                    newGame.GameProduction = DateTime.Now;
                    _unitOfWork.GameRepository.Insert(newGame);
                }
                else
                {

                    game.IsDeleted = true;
                    _unitOfWork.GameRepository.Update(game);
                }

                _unitOfWork.Save();

                return true;

            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs New(Genre genre)" + ex.Message);
                throw;
            }
        }

        public IEnumerable<Game> GetAllItems()
        {
            return GetAllItems("en");
        }

        public IEnumerable<Game> GetAllItems(string langCode)
        {
            try
            {
                var games = _unitOfWork.GameRepository.Get(x => true);
               // var product = _unitOfWork.ProductRepository.Get(x => true);
              //  var northwindGames = Mapper.Map<List<Game>>(product);
              var allGames = games;
                _logger.Debug("GameService.cs, result succsess - AllGames");
                allGames.Each(g => ChangeDescriptionToCurrentOrDefaultLanguage(langCode, g));
                return allGames.Where(x => !x.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.Error("GameService.cs, some error:" + "\n" + ex.Message);
                throw;
            }
        }

        public Game GetByKey(string key, string langCode = null)
        {
            try
            {
                if (langCode == null)
                {
                    _logger.Error("langCode is null, GameService.cs");
                    throw new ArgumentNullException("langCode");
                }
                if (string.IsNullOrWhiteSpace(key))
                {
                    _logger.Error("key is null, GameService.cs");
                    throw new ArgumentException("key is null");
                }
                var game = GetAllItems().SingleOrDefault(g => g.Key == key);
                ChangeDescriptionToCurrentOrDefaultLanguage(langCode, game);
                return game;
            }
            catch (Exception ex)
            {
                _logger.Error("Some error  GetByKey(string key) GameService.cs " + ex.Message);
                throw;
            }
        }

        // method wich change description dependency languages
        public void ChangeDescriptionToCurrentOrDefaultLanguage(string langCode, Game game)
        {

            if (langCode != null && langCode != "en")
            {
                var language = _unitOfWork.LanguageRepository.Get(l => l.Code == langCode).FirstOrDefault();
                if (language == null) return;
                var langId = language.Id;
                var gameLang =
                    _unitOfWork.GameLang.Get().FirstOrDefault(x => x.Id == game.Id && x.LanguageId == langId);
                if (gameLang != null)
                    game.Description = gameLang.Description;
            }
        }

        public Game FindGameInNorthwindDb(string gameKey)
        {
            try
            {
                int productId = GetProductIdByGameKey(gameKey);
                var product = _unitOfWork.ProductRepository.GetById(productId);
                if (product == null)
                    return null;
                var game = Mapper.Map<Game>(product);
                return game;
            }
            catch (Exception ex)
            {
                _logger.Error("Some error FindGameInNorthwindDb(string gameKey) GameService.cs " + ex.Message);
                throw;
            }
        }

        public int GetProductIdByGameKey(string gameKey)
        {
            try
            {
                int startIndex = gameKey.IndexOf(SettingsConst.Prefix) + SettingsConst.Prefix.Length;
                int productId = Convert.ToInt32(gameKey.Substring(startIndex));
                return productId;
            }
            catch (Exception ex)
            {
                _logger.Error("Some error GetProductIdByGameKey(string gameKey) GameService.cs " + ex.Message);
                throw;
            }
        }

        public int GetGamesCount()
        {
            try
            {
                return _unitOfWork.GameRepository.Get().ToList().Count;// + _unitOfWork.ProductRepository.Get().ToList().Count;
            }
            catch (Exception)
            {
                _logger.Error("some error: GameService.cs");
                throw;
            }
        }

        public bool ValidateKey(string key)
        {
            try
            {
                if (key == null)
                {
                    _logger.Error("key is null: GameService.cs");
                    throw new ArgumentNullException("key");
                }
                var game = _unitOfWork.GameRepository.Get(a => true).FirstOrDefault(x => x.Key == key);
                return game == null;
            }
            catch (Exception)
            {
                _logger.Error("some error: GameService.cs");
                throw;
            }
        }

        // fill object game=> genres platforms publisher
        public Game GetSideLinks(GameViewModel gameModel)
        {
            try
            {


                var genres = _genreService.GetAllItems();
                var publisher = _publisherService.GetAllItems();
                var platforms = _unitOfWork.PlatformRepository.Get();

                foreach (Genre g in gameModel.GenreList.Select(gName => genres.SingleOrDefault(x => x.Name == gName)))
                {
                    gameModel.Game.RegisterGenreToGame(g);
                }

                foreach (var pName in gameModel.PlatformList)
                {
                    Platform p = platforms.SingleOrDefault(x => x.Id == pName);
                    gameModel.Game.RegisterPlatformToGame(p);
                }

                Publisher publish = publisher.FirstOrDefault(x => x.CompanyName == gameModel.PublisherList.SingleOrDefault());
                gameModel.Game.RegisterPublisherToGame(publish);
                return gameModel.Game;
            }
            catch (Exception)
            {
                _logger.Error("some error: GameService.cs");
                throw;

            }
        }

        public IEnumerable<Game> Get(IFilterChain<IQueryable<Game>> filter, string langCode)
        {
            try
            {
                var result = _unitOfWork.GameRepository.Get().AsQueryable();
              //  var northwindResult = _unitOfWork.ProductRepository.Get().AsQueryable();
             //  List<Game> northwindGames = Mapper.Map<List<Game>>(northwindResult);

                var list = result.ToList();


                //list = filter.Execute(list.Union(northwindGames, new GameComparer())
                //    .Where(g => !g.IsDeleted).AsQueryable()).ToList();

                list.Each(g => ChangeDescriptionToCurrentOrDefaultLanguage(langCode, g));

                return list;
            }
            catch (Exception)
            {
                _logger.Error("some error: Get(IFilterChain<IQueryable<Game>> filter)");
                throw;
            }
        }

        public List<SelectListItem> GetAllItemsAsSelectListItems()
        {
            try
            {
                return GetAllItems().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Name
                }).ToList();


            }
            catch (Exception)
            {
                _logger.Error("some error: GameService.cs,  GetAllGameAsSelectListItems()");
                throw;
            }
        }

        public Game GetById(int id = 0)
        {
            return GetById(id, "en");
        }

        public Game GetById(int id = 0, string langCode = "en")
        {
            var game = _unitOfWork.GameRepository.GetById(id);

            if (langCode != "en" && String.IsNullOrEmpty(langCode))
                ChangeDescriptionToCurrentOrDefaultLanguage(langCode, game);

            return game;
        }

        public bool CheckOnItem(Game item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: GameService.cs CheckOnItem(Game item)");
                    throw new ArgumentNullException("item");

                }
                return GetAllItems().Any(g => String.CompareOrdinal(g.Name, item.Name) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GameService.cs CheckOnItem(Game item)" + ex.Message);
                throw;
            }
        }

        public IEnumerable<Game> Get(FilterArgs filters, string langCode = null, bool canReturnAnyLanguage = true)
        {
            var pipelinegames = new Pipeline<IQueryable<Game>>();
            var pipelineproducts = new Pipeline<IQueryable<Product>>();
            var sorting = new Pipeline<IQueryable<Game>>();

            InitializeFilters(pipelinegames, pipelineproducts, filters);
            InitializeSorting(sorting, filters);

            var games = pipelinegames.Execute(_unitOfWork.GameRepository.Get().AsQueryable()).AsEnumerable();
            var products = pipelineproducts.Execute(_unitOfWork.ProductRepository.Get().AsQueryable()).AsEnumerable();
            var gamesold = Mapper.Map<IEnumerable<Game>>(products).AsEnumerable();

            IEnumerable<Game> result = games.Union(gamesold, new GameComparer()).ToList().AsEnumerable();
            result = result.Where(a => !a.IsDeleted);
            result = sorting.Execute(result.AsQueryable());
            foreach (var game in result)
            {
                MapTranslationToGame(game, langCode, canReturnAnyLanguage);
            }
            return result;

        }


        private void InitializeSorting(IFilterChain<IQueryable<Game>> filter, FilterArgs model)
        {
            filter.Register(new PopularityFilter(model.FilterType));
        }


        private void InitializeFilters(IFilterChain<IQueryable<Game>> gameFilter, IFilterChain<IQueryable<Product>> productFilter, FilterArgs model)
        {
            gameFilter.Register(new GenreFilter(model.SelectedGenres ?? _genreService.GetAllItems().Select(x => x.Id)));
            gameFilter.Register(new PlatformFilter(model.SelectedPlatforms ?? _platformService.GetAllItems().Select(x => x.Id)));
            gameFilter.Register(new PublisherFilter(model.SelectedPublishers ?? _publisherService.GetAllItems().Select(x => x.Id)));
            gameFilter.Register(new PriceFilter((double)model.MinPrice, (double)model.MaxPrice));
            gameFilter.Register(new GameNameFilter(model.GameName));
            gameFilter.Register(new DateFilter((model.SelectedDate ?? new DatesFilter()).ToString()));


            productFilter.Register(new CategoryFilter(model.SelectedGenres));
            productFilter.Register(new SupplierFilter(model.SelectedPublishers));
            productFilter.Register(new ProductPriceRangeFilter(model.MinPrice, model.MaxPrice));
            productFilter.Register(new ProductNameFilter(model.GameName));
        }

        private void MapTranslationToGame(Game game, string langCode, bool canReturnAnyLanguage)
        {
            var languages = _unitOfWork.LanguageRepository.Get(a => a.Id == game.Id);
            var selectedLang = languages.FirstOrDefault(a => a.Code == langCode);
            if (selectedLang == null && canReturnAnyLanguage)
            {
                selectedLang = languages.FirstOrDefault();
            }
            if (selectedLang != null)
            {
                game.Name = selectedLang.Code;
                game.Description = selectedLang.Name;
            }
        }

    }
}
