using System.Web.Mvc;
using Model.Entities;
using BusinessLogicLayer.Services.IServices;
using NLog.Interface;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ExceptionControllerAttribute]
    public class GenreController : BaseController
    {

        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreServices, ILogger logger)
            : base(logger)
        {
            _genreService = genreServices;

        }


        /// <summary>
        ///  GET: /Genre/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_genreService.GetAllItems());
        }


        /// <summary>
        ///  GET: /Genre/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="northWindId">for NORTHWIND Id</param>
        /// <returns></returns>
        public ActionResult Details(int id , int northWindId)
        {
           
            Genre genre = _genreService.Get(id, northWindId);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }


        /// <summary>
        /// GET: /Genre/Create
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// POST: /Genre/Create
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            if (_genreService.CheckOnItem(genre))
            {
                ModelState.AddModelError("", "Genre with the same name already exsist");
                return View(genre);
            }

            _genreService.New(genre);
            return RedirectToAction("Index");

        }


        /// <summary>
        /// GET: /Genre/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="northWindId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id , int northWindId)
        {
            Genre genre = _genreService.Get(id, northWindId);

            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        /// <summary>
        /// POST: /Genre/Edit/5
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genreService.Update(genre);

                return RedirectToAction("Index");
            }
            return View(genre);
        }


        /// <summary>
        ///  GET: /Genre/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="northWindId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id , int northWindId)
        {
            Genre genre = _genreService.Get(id, northWindId);

            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }


        /// <summary>
        /// POST: /Genre/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="northWindId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, int northWindId)
        {
            Genre genre = _genreService.Get(id, northWindId);
            _genreService.Delete(genre);

            return RedirectToAction("Index");
        }


    }
}