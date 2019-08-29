using System.ServiceModel.Channels;
using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.SiteComparer;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NLog.Interface;

namespace BusinessLogicLayer.Services
{
    public class GenreService : IGenreService
    {

        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.Error("some error: GenreService.cs,  GetAllGenresAsSelectListItems()");
                throw;
            }
        }

        public IEnumerable<Genre> GetAllItems()
        {
            try
            {
                var genresFromMyDb = _unitOfWork.GenreRepository.Get();
                //var categoriesFromNorthWind = _unitOfWork.CategoryRepository.Get();
                //var mappedGenres = Mapper.Map<List<Genre>>(categoriesFromNorthWind);
                var allGenres = genresFromMyDb;
                return allGenres.Where(g => !g.IsDeleted);
            }
            catch (Exception)
            {
                _logger.Error("some error: GenreService.cs");
                throw;
            }
        }

        public void New(Genre genre)
        {
            try
            {
                _unitOfWork.GenreRepository.Insert(genre);

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs New(Genre genre)" + ex.Message);
                throw;
            }
        }

        public void Update(Genre genre)
        {
            try
            {
                if (genre.NorthWindId != 0)
                {

                    var oldCategory = _unitOfWork.CategoryRepository.GetById(genre.NorthWindId);
                    var transportGenre = Mapper.Map<Genre>(oldCategory);
                    transportGenre.Name = genre.Name;
                    transportGenre.NorthWindId = 0;
                    _unitOfWork.GenreRepository.Insert(transportGenre);
                }
                else
                {
                    _unitOfWork.GenreRepository.Update(genre);
                }

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs Update(Genre genre)" + ex.Message);
                throw;
            }
        }

        public void SetForGame(string gameKey, IEnumerable<string> genreNames)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                throw new ArgumentNullException("gameKey");
            }
            var genreNamesList = genreNames as IList<string> ?? genreNames.ToList();
            if (genreNames == null || !genreNamesList.Any())
            {
                return;
            }

            var game = _unitOfWork.GameRepository.Get().FirstOrDefault(x => String.Equals(gameKey, x.Key));
            if (game == null)
            {
                throw new KeyNotFoundException("gameKey");
            }

            if (game.Genres == null)
            {
                game.Genres = new List<Genre>();
            }

            var genres = _unitOfWork.GenreRepository.Get(a => genreNamesList.Contains(a.Name));
            IEnumerable<string> unsavedGenres = genreNamesList.Where(genreName => genres.FirstOrDefault(genre => genre.Name == genreName) == null);


            genres = genres.Union(GetGenresAddedFromNorthwindDB(unsavedGenres), new GenreComparer());

            foreach (var genre in genres)
            {
                game.Genres.Add(genre);
            }
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Save();
        }

        private IEnumerable<Genre> GetGenresAddedFromNorthwindDB(IEnumerable<string> categoryNames)
        {
            var categories = _unitOfWork.CategoryRepository.Get(category => categoryNames.Contains(category.CategoryName));
            if (categories == null)
            {
                return new List<Genre>();
            }
            var genres = Mapper.Map<IEnumerable<Genre>>(categories);
            foreach (Genre genre in genres)
            {
                _unitOfWork.GenreRepository.Insert(genre);
            }
            return genres;
        }

        public Genre Get(int id, int northWind)
        {
            return northWind != 0 ? GetFromNorthWind(northWind) : GetById(id);
        }

        public Genre GetById(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException("Id can not be 0");
                }
                return _unitOfWork.GenreRepository.GetById(id);

            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs GetById(int id = 0)" + ex.Message);
                throw;
            }
        }

        public Genre GetFromNorthWind(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException("Id can not be 0");
                }
                return Mapper.Map<Genre>(_unitOfWork.CategoryRepository.GetById(id));

            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs Genre GetFromNorthWind(int id)" + ex.Message);
                throw;
            }
        }

        public bool Delete(Genre genre)
        {
            try
            {
                if (genre.NorthWindId == 0)
                {
                    genre.IsDeleted = true;

                    _unitOfWork.GenreRepository.Update(genre);
                }
                else
                {
                    var oldCategory = _unitOfWork.CategoryRepository.GetById(genre.NorthWindId);
                    var transportGenre = Mapper.Map<Genre>(oldCategory);
                    transportGenre.IsDeleted = true;
                    transportGenre.NorthWindId = 0;
                    _unitOfWork.GenreRepository.Insert(transportGenre);
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

        public bool CheckOnItem(Genre item)
        {
            try
            {
                return GetAllItems().Any(g => String.CompareOrdinal(g.Name, item.Name) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs CheckOnItem(Genre item)" + ex.Message);
                throw;
            }
        }



    }
}
