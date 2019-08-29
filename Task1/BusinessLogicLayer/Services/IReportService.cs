using Model;
using Model.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IReportService
    {
       
        List<Comment> getCommentForEntity(int KeyEntity, EntityType entityType, DateTime fromDate, DateTime toDate);
        List<EntityView> getViewForEntity(int KeyEntity, EntityType entityType, DateTime fromDate, DateTime toDate);
    }
}
