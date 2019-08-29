using BusinessLogicLayer.Services.IServices;
using Model.Entities;
using Yevhenii_KoliesnikTask1.Authorization.AuthorInterface;

namespace Yevhenii_KoliesnikTask1.Authorization
{
    public class UserIndentity:System.Security.Principal.IIdentity, IUserProvider
    {
        /// <summary>
        /// Current password
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// type class for user
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        /// <summary>
        /// authoriastion or not
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        /// <summary>
        /// unique user name
        /// </summary>
        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Email;
                }
               
                return "anonym";
            }
        }

        /// <summary>
        /// initialize by name
        /// </summary>
        /// <param name="login">user name [email]</param>
        /// <param name="auth"></param>
        public void Init(string login, IAuthenticationService auth)
        {
            if (!string.IsNullOrEmpty(login))
            {
                User = auth.GetUser(login);
            }
        }
    }
}