using BusinessLogicLayer.Services;
using DAL;
using Model;
using Model.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    public class ReportController : Controller
    {

        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();  
        protected IReportService reportService;
      

       public ReportController( IReportService reportServices)
       {
          reportService = reportServices;
       }


      
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
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
            try
            {
                switch (condition)
                {
                    case ConditionType.Commente:
                        {
                            var commenteCondition = reportService.getCommentForEntity(KeyEntity, entity, fromDate, toDate);
                            logger.Info("++++++++++ Report with that parametrs: {0}, {1}, {2}, {3}, {4} +++++++++++++", KeyEntity, entity, condition, fromDate, toDate);
                            foreach (var comment in commenteCondition)
                            {
                                logger.Info("{0} - {1}\n{2}\n{3}\n\n", comment.CommentId, comment.AuthorName, comment.Body, comment.DateComment.ToShortDateString());
                            }
                            break;
                        }
                    case ConditionType.Popular:
                        {
                            var popularCondition = reportService.getViewForEntity(KeyEntity, entity, fromDate, toDate);
                            logger.Info("++++++++++ Report with that parametrs: {0}, {1}, {2}, {3}, {4} +++++++++++++", KeyEntity, entity, condition, fromDate, toDate);
                            foreach (var view in popularCondition)
                            {
                                logger.Info("{0} - {1}\n{2}\n{3}\n\n", view.Id, view.IdEntity, view.TypeEntity, view.DateView.ToShortDateString());
                            }
                            break;
                        }
                    default:
                        {
                            logger.Error("Condition -> problem");
                            return Json(new { result = "Error! Sorry ... " });
                        }
                }
                return Json("Report was created! Show log files");

            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Report, Action: Report" + "\n" + ex.Message);
                return Json(new { result = "Error! Sorry ... " });
            }
        }

    }
}
