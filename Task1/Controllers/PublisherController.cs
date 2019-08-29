using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using NLog.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [ExceptionControllerAttribute]
    [Authorize(Roles = "Publisher, Manager")]
    public class PublisherController : BaseController
    {
       
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherServices, ILogger logger):base(logger)
        {
            _publisherService = publisherServices;

        }

        [HttpGet]
        public ActionResult AllPublishers()
        {
            
            var publishers = _publisherService.GetAllItems();
            return View("AllPublishers", publishers.ToList());
        }

    
        /// <summary>
        /// GET: /Publisher/new
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("new")]
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        ///  User can create game (POST URL: /games/new). 
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("new")]
        public ActionResult New(Publisher publisher)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Fill field correctly");
                    return View(publisher);
                }
               
               
                    _publisherService.New(publisher);
                    return RedirectToRoute(new { controller = "Publisher", action = "AllPublishers", id = UrlParameter.Optional });
               
            }
            catch (Exception ex)
            {
                Logger.Error("some error: Controller: Publisher, Action: New" + "\n" + ex.Message);
                return new HttpNotFoundResult();
            }


        }

        [HttpGet]
        public ActionResult PublisherDetails(string companyName, int northWindId)
        {
            try
            {
                Publisher publisher = _publisherService.GetPublisher(companyName);
                if (publisher == null)
                {
                    Logger.Error("PublisherController.cs, Publisher not found");
                    throw new ArgumentException("Publisher not found");
                }

                return View(publisher);
            }
            catch (Exception)
            {
                Logger.Error("PublisherController.cs, some error");
                throw;
            }

        }

        [HttpGet]
        public ActionResult Update(string companyName, int northWindId)
        {
            try
            {
                var publisher = _publisherService.GetPublisher(companyName);
                var mapmodel = Mapper.Map<PublisherEditModel>(publisher);
                return View(mapmodel);
            }
            catch (Exception ex)
            {
                Logger.Error("PublisherController.cs, some error" + ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult Update(PublisherEditModel editModel)
        {
            try
            {
                Publisher publisher = Mapper.Map<Publisher>(editModel);
                _publisherService.Update(publisher);
                return RedirectToRoute("AllPublishers");
            }
            catch (Exception ex)
            {
                Logger.Error("PublisherController.cs, some error Update(PublisherEditModel editModel)" + ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(string companyName, int northWindId)
        {
            try
            {
                var p = _publisherService.GetPublisher(companyName);
                return View("Delete", p);
            }
            catch (Exception ex)
            {
                Logger.Error("some error: Controller: Publisher, Action: Remove" + "\n" + ex.Message);
                return new HttpNotFoundResult();
            }
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string companyName, int northWindId)
        {
            try
            {
                Publisher p = _publisherService.GetPublisher(companyName);
                _publisherService.Delete(p);

                return RedirectToRoute("AllPublishers");
            }
            catch (Exception ex)
            {
                Logger.Error("PublisherController.cs, some error Update(PublisherEditModel editModel)" + ex.Message);
                throw;
            }
        }
    }
}
