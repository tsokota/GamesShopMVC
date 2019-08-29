using System;
using System.Web;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Authorization.AuthorInterface;

namespace Yevhenii_KoliesnikTask1.Authorization
{
    public class AuthHttpModule : System.Web.IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(Authenticate);
        }

        private void Authenticate(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            var auth = DependencyResolver.Current.GetService<IAuthentication>();
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }

        public void Dispose()
        {
        }
    }

}