using BusinessLogicLayer.Services;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Reporting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Controllers;
namespace GameUnitTest
{
    [TestClass]
    public class ReportControllerTest
    {


        UnitOfWork uof = new UnitOfWork();       
        ReportService reportService;
        ReportController controller;



        [TestInitialize]
        public void Init()
        {
            var views = new List<EntityView>();


            #region InitView

            var view = new EntityView { Id = 1, DateView = new DateTime(2014,08,28),IdEntity="1",TypeEntity=EntityType.Game};
            views.Add(view);
           
            view = new EntityView { Id = 1, DateView = new DateTime(2014, 08, 28), IdEntity = "1", TypeEntity = EntityType.Game };
            views.Add(view);
            view = new EntityView { Id = 2, DateView = new DateTime(2014, 08, 28), IdEntity = "1", TypeEntity = EntityType.Game };
            views.Add(view);
            view = new EntityView { Id = 3, DateView = new DateTime(2014, 08, 28), IdEntity = "1", TypeEntity = EntityType.Game };
            views.Add(view);
            view = new EntityView { Id = 4, DateView = new DateTime(2014, 08, 28), IdEntity = "1", TypeEntity = EntityType.Game };
            views.Add(view);
       
            #endregion


            var reportRepo = new Mock<GenericRepository<EntityView>>();
            reportRepo.Setup(x => x.Get(a => true, null, "")).Returns(views.AsQueryable);
            reportService = new ReportService(uof);
            controller = new ReportController(reportService);
       
        }

        [TestMethod]
        public void ReportFail()
        {
            var res = new { result = "Error! Sorry ... " };

            var actionResult = controller.Report(1,EntityType.Game,ConditionType.Commente,new DateTime(2014,08,27), new DateTime(2014,08,29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());

            var actionResult2 = controller.Report(2, EntityType.Game, ConditionType.Popular, new DateTime(2014, 08, 27), new DateTime(2014, 08, 29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());

            var actionResult3 = controller.Report(3, EntityType.Game, ConditionType.Popular, new DateTime(2014, 08, 27), new DateTime(2014, 08, 29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

      //  [TestMethod]
        public void ReportSuccess()
        {
            var res = "Report was created! Show log files";

            var actionResult = controller.Report(1, EntityType.Game, ConditionType.Commente, new DateTime(2014, 08, 27), new DateTime(2014, 08, 29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());

            var actionResult2 = controller.Report(2, EntityType.Game, ConditionType.Popular, new DateTime(2014, 08, 27), new DateTime(2014, 08, 29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());

            var actionResult3 = controller.Report(3, EntityType.Game, ConditionType.Popular, new DateTime(2014, 08, 27), new DateTime(2014, 08, 29)) as JsonResult;
            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }
    }
}
