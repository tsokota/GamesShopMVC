using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using DAL;

namespace BusinessLogicLayer.Services
{
    public class ShipperService : IShipperService
    {
        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _unitOfWork;

        public ShipperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<Shipper> AllShipper()
        {
            try
            {
                return _unitOfWork.ShipperRepository.Get().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: ShipperService.cs,  AllShipper()" +ex.Message);
                throw;
            }
                
        }
    }
}
