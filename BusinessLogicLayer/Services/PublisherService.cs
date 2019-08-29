using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.SiteComparer;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NLog.Interface;

namespace BusinessLogicLayer.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public PublisherService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void New(Publisher publisher)
        {
            try
            {
                if (publisher == null)
                {
                    _logger.Error("publisher == null, PublisherService.cs");
                    throw new ArgumentNullException("publisher");
                }
                _unitOfWork.PublisherRepository.Insert(publisher);
                _unitOfWork.Save();
                _logger.Debug("PublisherService.cs, result succsess - added new publisher: {0}, {1}", publisher.CompanyName, publisher.Id);
            }
            catch (Exception ex)
            {
                _logger.Error("PublisherService.cs, some error:" + "\n" + ex.Message);
            }


        }

        public Publisher GetPublisher(string companyName)
        {
            try
            {

                var publisher = GetAllItems().SingleOrDefault(p => p.CompanyName == companyName);
                return publisher;

            }
            catch (Exception)
            {
                _logger.Error("PublisherService.cs, some error");
                throw;
            }
        }

        public IEnumerable<Publisher> GetAllItems()
        {
            try
            {
                var publisherFromMyDb = _unitOfWork.PublisherRepository.Get();
                //var suppliersFromNorthWind = _unitOfWork.SupplierRepository.Get();
                //var mappedPublisher = Mapper.Map<List<Publisher>>(suppliersFromNorthWind);
                var allGenres = publisherFromMyDb; //publisherFromMyDb.Union(mappedPublisher, new PublisherComparer()).ToList();
                return allGenres.Where(g => !g.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PublisherService.cs GetAllItems()" + ex.Message);
                throw;
            }
        }

        public void Update(Publisher publisher)
        {
            try
            {
                if (publisher.NorthWindId != 0)
                {

                    var oldPublisher = _unitOfWork.SupplierRepository.GetById(publisher.NorthWindId);

                    oldPublisher.CompanyName = publisher.CompanyName;
                    oldPublisher.ContactTitle = publisher.Description;
                    oldPublisher.HomePage = publisher.HomePage;
                    var transportPublisher = Mapper.Map<Publisher>(oldPublisher);
                    _unitOfWork.PublisherRepository.Insert(transportPublisher);
                }
                else
                {
                    _unitOfWork.PublisherRepository.Update(publisher);
                }

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: PubliserService.cs Update(Publisher publisher)" + ex.Message);
                throw;
            }
        }

        public List<SelectListItem> GetAllItemsAsSelectListItems()
        {
            try
            {
                return GetAllItems().Select(a => new SelectListItem
                {
                    Text = a.CompanyName,
                    Value = a.CompanyName,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("PublisherService.cs, some error" + ex.Message);
                throw;
            }
        }

     

        public bool Delete(Publisher publisher)
        {
            try
            {
                if (publisher.NorthWindId == 0)
                {
                    publisher.IsDeleted = true;
                    _unitOfWork.PublisherRepository.Update(publisher);
                }
                else
                {
                    var oldSupplier = _unitOfWork.SupplierRepository.GetById(publisher.NorthWindId);
                    var transportPublisher = Mapper.Map<Publisher>(oldSupplier);
                    transportPublisher.IsDeleted = true;
                    _unitOfWork.PublisherRepository.Update(transportPublisher);
                }

                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs Delete(Publisher publisher)" + ex.Message);
                throw;
            }
        }

        public Publisher GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    return _unitOfWork.PublisherRepository.GetById(id);
                }
                return Mapper.Map<Publisher>(_unitOfWork.SupplierRepository.GetById(id * -1));
            }
            catch (Exception)
            {

                _logger.Error("PublisherService.cs, GetPublisher(int CompanyId) some error");
                throw;
            }
        }

        public bool CheckOnItem(Publisher item)
        {
            try
            {
                return GetAllItems().Any(p => String.CompareOrdinal(p.CompanyName, p.CompanyName) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs CheckOnItem(Genre item)" + ex.Message);
                throw;
            }
        }

        public void SetForGame(string gameKey, string companyName)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                throw new ArgumentNullException("gameKey");
            }
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentNullException("companyName");
            }

            var game = _unitOfWork.GameRepository.Get().FirstOrDefault(x => x.Key == gameKey);
            if (game == null)
            {
                throw new KeyNotFoundException("gameKey");
            }

            var publisher = _unitOfWork.PublisherRepository.Get().FirstOrDefault(x => x.CompanyName == companyName);
            if (publisher == null)
            {
                publisher = AddSupplierAsPublisher(companyName);
                if (publisher == null)
                {
                    throw new KeyNotFoundException("companyName");
                }
            }
            game.Publisher = publisher;
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Save();
        }

        private Publisher AddSupplierAsPublisher(string companyName)
        {
            var supplier = _unitOfWork.SupplierRepository.Get().FirstOrDefault(x => x.CompanyName == companyName);
            var publisher = Mapper.Map<Publisher>(supplier);
            _unitOfWork.PublisherRepository.Insert(publisher);
            _unitOfWork.Save();
            return publisher;
        }

    }
}
