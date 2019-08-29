using BusinessLogicLayer.Services.IServices;
using Model.Reporting;
using NLog.Interface;
using System;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [ExceptionControllerAttribute]
    [Authorize(Roles = "Administrator")]
    public class ReportController : BaseController
    {
        private IReportService reportService;
        private readonly ILogger _logger;
        public ReportController(IReportService reportServices, ILogger logger)
            : base(logger)
        {
            reportService = reportServices;
            _logger = logger;
        }

        /// <summary>
        /// GET URL: /report/[entity]. Display [entity] statistics by parameters. 
        /// </summary>
        /// <param name="entity">could be game, genre or something else in the future</param>
        /// <param name="condition">commented, popular(by viewing page)</param>
        /// <param name="fromDate">From:</param>
        /// <param name="toDate">To:</param>
        /// <returns>Entities and statistics</returns>
        /// 
        [HttpGet]
        [ActionName("report")]
        public ActionResult Report(int KeyEntity, EntityType entity, ConditionType condition, DateTime fromDate, DateTime toDate)
        {
            if (condition == ConditionType.Commente)
                MakeCommentReport(KeyEntity, entity, condition, fromDate, toDate);
            else
                MakePopularReport(KeyEntity, entity, condition, fromDate, toDate);
            return Json("Report was created! Show log files");
        }

        [NonAction]
        public void MakeCommentReport(int KeyEntity, EntityType entity, ConditionType condition, DateTime fromDate, DateTime toDate)
        {
            var commenteCondition = reportService.GetCommentForEntity(KeyEntity, entity, fromDate, toDate);
            _logger.Info("++++++++++ Report with that parametrs: {0}, {1}, {2}, {3}, {4} +++++++++++++", KeyEntity, entity, condition, fromDate, toDate);
            foreach (var comment in commenteCondition)
            {
                _logger.Info("{0} - {1}\n{2}\n{3}\n\n", comment.Id, comment.AuthorName, comment.Body, comment.DateComment.ToShortDateString());
            }
        }

        [NonAction]
        public void MakePopularReport(int KeyEntity, EntityType entity, ConditionType condition, DateTime fromDate, DateTime toDate)
        {
            var popularCondition = reportService.GetViewForEntity(KeyEntity, entity, fromDate, toDate);
            _logger.Info("++++++++++ Report with that parametrs: {0}, {1}, {2}, {3}, {4} +++++++++++++", KeyEntity, entity, condition, fromDate, toDate);
            foreach (var view in popularCondition)
            {
                _logger.Info("{0} - {1}\n{2}\n{3}\n\n", view.Id, view.IdEntity, view.TypeEntity, view.DateView.ToShortDateString());
            }
        }

    }
}
