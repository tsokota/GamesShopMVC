using BusinessLogicLayer.ViewModel;
using NLog.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.WebApi
{
    [ExceptionControllerAttribute]
    public class AccountController : BaseWebApiController
    {


        public AccountController(ILogger logger)
            : base(logger)
        {

        }

        [HttpPost]
        public HttpResponseMessage Login(LoginView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Auth.Login(model.Name, model.Password, model.IsPersistent);
                    if (user != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                Logger.Error("HttpResponseMessage Login(LoginView model) some error " + ex.Message);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
