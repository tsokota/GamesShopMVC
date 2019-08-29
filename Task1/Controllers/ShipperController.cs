using BusinessLogicLayer.Services.IServices;
using NLog.Interface;
using System;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [ExceptionControllerAttribute]
    [Authorize(Roles = "Administrator")]
    public class ShipperController : BaseController
    {
        //
        // GET: /Shippers/


     
        private readonly IShipperService _service;
        public  ShipperController(IShipperService service, ILogger logger):base(logger)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            try
            {
                var shippers = _service.AllShipper();
                return View(shippers);
            }
            catch (Exception ex)
            {
                Logger.Error("Some error inIndex()[Get] ShipperController.cs" + ex.Message);
                return new HttpNotFoundResult();
            }
        }

    }
}
