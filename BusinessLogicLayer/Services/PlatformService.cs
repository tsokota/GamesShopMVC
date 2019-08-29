using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLogicLayer.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        public PlatformService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
           _logger = logger;
        }

        public List<SelectListItem> GetAllItemsAsSelectListItems()
        {
            try
            {
                return _unitOfWork.PlatformRepository.Get(x => true).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs  GetAllItemsAsSelectListItems()" + ex.Message);
                throw;
            }
        }

        public IEnumerable<Platform> GetAllItems()
        {
            try
            {
                return _unitOfWork.PlatformRepository.Get(x => true);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs  GetAllItems()" + ex.Message);
                throw;
            }
        }

        public void New(Platform item)
        {
            try
            {
                if(item==null)
                {
                    _logger.Error("item is null: PlatformService.cs  New(Platform item)");
                    throw new ArgumentNullException("item");
                }
                _unitOfWork.PlatformRepository.Insert(item);

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs  New(Platform item)" + ex.Message);
                throw;
            }
        }

        public void Update(Platform item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: PlatformService.cs Update(Platform item)");
                    throw new ArgumentNullException("item");
                }
                var platform = _unitOfWork.PlatformRepository.GetById(item.Id);
                _unitOfWork.PlatformRepository.Update(platform);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs  Delete(Platform item)"+ex.Message);
                throw;
            }
        }

        public bool Delete(Platform item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: PlatformService.cs  Delete(Platform item)");
                    throw new ArgumentNullException("item");
                }
                item.IsDeleted = true;
                Update(item);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {

                _logger.Error("some error: PlatformService.cs  Delete(Platform item)"+ex.Message);
                throw;
            }
        }

        public Platform GetById(int id)
        {
            try
            {
                return _unitOfWork.PlatformRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs  Delete(Platform item)" + ex.Message);
                throw new ArgumentNullException("id");
            }
        }

        public bool CheckOnItem(Platform item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: PlatformService.cs  CheckOnItem(Platform item)");
                    throw new ArgumentNullException("item");
                }
                return GetAllItems().Any(p => String.CompareOrdinal(p.Name, item.Name) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PlatformService.cs CheckOnItem(Platform item)" + ex.Message);
                throw;
            }
        }

        public void SetForGame(string gameKey, IEnumerable<string> platformNames)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                throw new ArgumentNullException("gameKey");
            }
            var game = _unitOfWork.GameRepository.Get().FirstOrDefault(x => x.Key == gameKey);
            if (game == null)
            {
                throw new KeyNotFoundException("gameKey");
            }
            if (platformNames == null || !platformNames.Any())
            {
                return;
            }
            if (game.Platforms == null)
            {
                game.Platforms = new List<Platform>();
            }
            foreach (var platformName in platformNames)
            {
                var platform = _unitOfWork.PlatformRepository.Get().FirstOrDefault(x => x.Name== platformName);
                game.Platforms.Add(platform);
            }
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Save();
        }
    }
}
