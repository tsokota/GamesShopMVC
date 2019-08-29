using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model.Entities;
using Model.Reporting;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services
{
    public class ReportService : IReportService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public ReportService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// get data from context about comment for DaetSpan
        /// </summary>
        /// <param name="keyEntity">Key entity</param>
        /// <param name="entityType">
        /// </param>
        /// <param name="fromDate">From:</param>
        /// <param name="toDate">To:</param>
        /// <returns>Comment of this entity for date period</returns>
        public List<Comment> GetCommentForEntity(int keyEntity, EntityType entityType, DateTime fromDate, DateTime toDate)
        {
            try
            {
                switch (entityType)
                {
                    case EntityType.Game:
                        {
                            return _unitOfWork.GameRepository.GetById(keyEntity).Comments.Where(comment => comment.DateComment >= fromDate && comment.DateComment <= toDate).ToList();

                        }
                    case EntityType.Genre:
                        {
                            throw new NotImplementedException("");
                        }
                    case EntityType.Platform:
                        {
                            throw new NotImplementedException("");
                        }
                    default:
                        {
                            _logger.Error("ReportService.cs, EntityType error");
                            throw new ArgumentException("EntityType Error");

                        }
                }
            }
            catch (Exception)
            {
                _logger.Error("ReportService.cs, some error");
                throw;

            }

        }

        public List<EntityView> GetViewForEntity(int keyEntity, EntityType entityType, DateTime fromDate, DateTime toDate)
        {
            try
            {
                switch (entityType)
                {
                    case EntityType.Game:
                        {

                            return _unitOfWork.ViewRepository.Get(a => true).Where(view => view.DateView >= fromDate && view.DateView <= toDate &&
                                view.TypeEntity == entityType && view.IdEntity == keyEntity.ToString()).ToList();

                        }
                    case EntityType.Genre:
                        {
                            throw new NotImplementedException("");
                        }
                    case EntityType.Platform:
                        {
                            throw new NotImplementedException("");
                        }
                    default:
                        {
                            _logger.Error("ReportService.cs, EntityType error");
                            throw new ArgumentException("EntityType Error");

                        }
                }
            }
            catch (Exception)
            {
                _logger.Error("ReportService.cs, some error");
                throw;
            }


        }

    }

}