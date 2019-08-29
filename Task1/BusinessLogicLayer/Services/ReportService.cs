using Model;
using Model.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReportService:IReportService
    {
        



        private UnitOfWork unitOfWork;

        public ReportService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ReportService()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }


        /// <summary>
        /// get data from context about comment for DaetSpan
        /// </summary>
        /// <param name="KeyEntity">Key entity</param>
        /// <param name="entityType">
        /// </param>
        /// <param name="fromDate">From:</param>
        /// <param name="toDate">To:</param>
        /// <returns>Comment of this entity for date period</returns>
        public List<Comment> getCommentForEntity(int KeyEntity, EntityType entityType, DateTime fromDate, DateTime toDate)
        {
            switch (entityType)
            {
                case EntityType.Game:
                    {
                        return unitOfWork.GameRepository.GetByID(KeyEntity).Comments.Where(comment => comment.DateComment >= fromDate && comment.DateComment <= toDate).ToList();
                    
                    }
                case EntityType.Genre:
                    {
                      throw new  NotImplementedException("");
                    }
                case EntityType.Platform:
                    {
                        throw new NotImplementedException("");
                    }
                default:
                    {
                        throw new ArgumentException("EntityType Error");
                       
                    }
            }
           
        }

        public List<EntityView> getViewForEntity(int KeyEntity, EntityType entityType, DateTime fromDate, DateTime toDate)
        {
            switch (entityType)
            {
                case EntityType.Game:
                    {
                        
                    return unitOfWork.ViewRepository.Get(a=>true).Where(view => view.DateView >= fromDate && view.DateView <= toDate && 
                        view.TypeEntity==entityType && view.IdEntity == KeyEntity.ToString()).ToList();

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
                        throw new ArgumentException("EntityType Error");

                    }
            }

        }

    }
}
