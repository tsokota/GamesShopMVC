using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IAuthenticationService
    {
        User Login(string email, string password);

        User Login(string userName);

        User GetUser(string email);

        bool EmailExist(string email);

        void CreateUser(User user);

        Role GetRole(string role);

        IEnumerable<User> GetUsers();

        User GetUser(int  userId);

        IEnumerable<Role> GetRoles();

        void DeleteUser(User user);

        Role GetRole(int roleId);

        void DeleteRole(Role role);

        void CreateRole(Role role);

        bool RoleExist(string role);

        bool LoginExist(string login);

        bool UpdateUser(User user, IEnumerable<int> roles);

        bool UpdateRole(Role role, List<int> users);

        bool InRoles(string roles, User user);
      
    }
}
