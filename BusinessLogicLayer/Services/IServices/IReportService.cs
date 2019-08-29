using Model.Entities;
using Model.Reporting;
using System;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IReportService
    {      
        List<Comment> GetCommentForEntity(int keyEntity, EntityType entityType, DateTime fromDate, DateTime toDate);
        List<EntityView> GetViewForEntity(int keyEntity, EntityType entityType, DateTime fromDate, DateTime toDate);
    }
}
