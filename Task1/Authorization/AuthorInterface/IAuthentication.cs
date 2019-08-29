using Model.Entities;
using System.Web;

namespace Yevhenii_KoliesnikTask1.Authorization.AuthorInterface
{
    public interface IAuthentication
    {
        /// <summary>
        /// Context (retriving access to coocies and request)
        /// </summary>
        HttpContext HttpContext { get; set; }

        /// <summary>
        /// login procedure
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <param name="isPersistent">all time registration or not</param>
        /// <returns></returns>
        User Login(string login, string password, bool isPersistent);

        /// <summary>
        /// login without password 
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns>
        User Login(string login);

        /// <summary>
        /// exit
        /// </summary>
        void LogOut();

        /// <summary>
        /// Data about current user
        /// </summary>
        System.Security.Principal.IPrincipal CurrentUser { get; }
    }
}


