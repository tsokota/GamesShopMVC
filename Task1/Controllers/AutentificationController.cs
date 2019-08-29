using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;
using Yevhenii_KoliesnikTask1.Mappers;
using Yevhenii_KoliesnikTask1.Tools;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [AllowAnonymous]
    [ExceptionControllerAttribute]
    public class AutentificationController : BaseController
    {
        private const string CookieName = "AUTH_COOKIE";
        // GET: /Auth/     
        readonly IAuthenticationService _service;
        public AutentificationController(IAuthenticationService service, ILogger logger)
            : base(logger)
        {
            _service = service;
        }


        public ActionResult UserLogin()
        {

            return PartialView(CurrentUser);

        }

        [HttpGet]
        public ActionResult Login()
        {

            return View(new LoginView());

        }

        [HttpPost]
        public ActionResult Login(LoginView loginView)
        {

            if (ModelState.IsValid)
            {
                var user = Auth.Login(loginView.Name, loginView.Password, loginView.IsPersistent);
                if (user != null)
                {
                    return RedirectToAction("AllGames", "Game");
                }
                ModelState["Password"].Errors.Add("Password missmatch");
            }
            return View(loginView);

        }

        public ActionResult Logout()
        {

            Auth.LogOut();
            return RedirectToAction("AllGames", "Game");

        }

        [HttpGet]
        public ActionResult Register()
        {

            var newUserView = new UserView();
           // newUserView.BirthdateDate = new DateTime(1970, 1, 1);
            return View(newUserView);

        }

        [HttpPost]
        public ActionResult Register(UserView userView)
        {

            if (userView.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Textfrom image entered not correct");
            }
            var anyUser = _service.EmailExist(userView.Email);
            if (anyUser)
            {
                ModelState.AddModelError("Email", "User with this email already existed");
            }
            if (ModelState.IsValid)
            {
                var user = (User)MainMapper.Map(userView, typeof(UserView), typeof(User));
                user.UserRoles = new List<Role> { _service.GetRole("User") };
                _service.CreateUser(user);
                return RedirectToAction("AllGames", "Game");
            }
            return View(userView);

        }


        // Generation new Captcha for check user input
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Arial");

            // Change the response headers to output a JPEG image.
            Response.Clear();
            Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }
    }

}

