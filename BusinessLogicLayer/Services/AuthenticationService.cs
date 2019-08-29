using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AuthenticationService(IUnitOfWork unitOfWork, ILogger logger)
        {
           _unitOfWork = unitOfWork;
           _logger = logger;
        }  

        public User Login(string email, string password)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get().FirstOrDefault(p => String.Compare(p.Login, email, StringComparison.OrdinalIgnoreCase) == 0 && p.Password == password);
                return user;
            }
            catch(Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs Login(string, string)"+ex.Message);
                throw;
            }
        }

        public User Login(string userName)
        {
            try
            {
                return _unitOfWork.UserRepository.Get().FirstOrDefault(p => String.Compare(p.Email, userName, StringComparison.OrdinalIgnoreCase) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs Login(string userName)" + ex.Message);
                throw;
            }
        }

        public User GetUser(string login)
        {
            try
            {
                return _unitOfWork.UserRepository.Get().FirstOrDefault(p => String.Compare(p.Login, login, StringComparison.OrdinalIgnoreCase) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs GetUser(string)" + ex.Message);
                throw;
            }
        }

        public bool EmailExist(string email)
        {
            try
            {
                return _unitOfWork.UserRepository.Get().Any(p => String.CompareOrdinal(p.Email, email) == 0);
            }
            catch(Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs EmailExist(string email)" + ex.Message);
                throw;
            }
        }

        public bool LoginExist(string login)
        {
            try
            {
                return _unitOfWork.UserRepository.Get().Any(p => String.CompareOrdinal(p.Login, login) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs EmailExist(string email)" + ex.Message);
                throw;
            }
        }

        public bool RoleExist(string role)
        {
            try
            {
                return _unitOfWork.RoleRepository.Get().Any(p => String.CompareOrdinal(p.NameRole, role) == 0);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs EmailExist(string email)" + ex.Message);
                throw;
            }
        }

        public void CreateUser(User user)
        {
            try
            {
               _unitOfWork.UserRepository.Insert(user);
               _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs CreateUser(User user)" + ex.Message);
                throw;
            }
        }

        public Role GetRole(string role)
        {
            try
            {
                return _unitOfWork.RoleRepository.Get().FirstOrDefault(rol => rol.NameRole == role);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs GetRole(string role)" + ex.Message);
                throw;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _unitOfWork.UserRepository.Get();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs IEnumerable<User> GetUsers()" + ex.Message);
                throw;
            }
        }

        public User GetUser(int userId)
        {
            try
            {
                return _unitOfWork.UserRepository.GetById(userId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs GetUser(int userId)" + ex.Message);
                throw;
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _unitOfWork.RoleRepository.Get();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs IEnumerable<Role> GetRoles()" + ex.Message);
                throw;
            }
        }

        public Role GetRole(int roleId)
        {
            try
            {
                return _unitOfWork.RoleRepository.GetById(roleId);
            }
            catch(Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs GetRole(int roleId)" + ex.Message);
                throw;
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }
                _unitOfWork.UserRepository.Delete(user);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs DeleteUser(User user)" + ex.Message);
                throw;
            }
        }

        public void DeleteRole(Role role)
        {
            try
            {
                if (role == null)
                {
                    _logger.Error("role is null AuthentificationService.cs");
                    throw new ArgumentNullException("role");
                }
                _unitOfWork.RoleRepository.Delete(role);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs DeleteRole(Role role)" + ex.Message);
                throw;
            }
        }

        public void CreateRole(Role role)
        {
            try
            {
                if (role == null)
                {
                    _logger.Error("role is null AuthentificationService.cs");
                    throw new ArgumentNullException("role");
                }
                _unitOfWork.RoleRepository.Insert(role);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs CreateRole(Role role)" + ex.Message);
                throw;
            }
        }

        public  bool UpdateUser(User user, IEnumerable<int> roles)
        {
            try
            {
                if (user == null)
                {
                    _logger.Error("UpdateUser(User user, IEnumerable<int> roles) user is null ");
                    throw new ArgumentNullException("user");
                }
                User u = _unitOfWork.UserRepository.GetById(user.Id);
                user.UserRoles = new List<Role>();
                foreach (var r in roles)
                    user.UserRoles.Add(_unitOfWork.RoleRepository.GetById(r));
                _unitOfWork.UserRepository.AttachStubs(user.UserRoles.ToArray());

                foreach (var role in _unitOfWork.RoleRepository.Get().Where(role => role.UsersInRole.Any(x => x.Id == user.Id)))
                {
                    role.UsersInRole.Remove(role.UsersInRole.First(x => x.Id == user.Id));
                }
                _unitOfWork.Save();
                foreach (var prop in u.GetType().GetProperties())
                {
                    prop.SetValue(u, user.GetType().GetProperty(prop.Name).GetValue(user));
                }

                _unitOfWork.UserRepository.Update(u);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs UpdateUser(User user, IEnumerable<int> roles))" + ex.Message);
                throw;
            }
        }

        public  bool UpdateRole(Role role, List<int> users)
        {
            try
            {
                if (role == null)
                {
                    _logger.Error("UpdateRole(Role role, List<int> users) role is null ");
                    throw new ArgumentNullException("role");
                }
                Role r = _unitOfWork.RoleRepository.GetById(role.Id);
                role.UsersInRole = new List<User>();
                foreach (var u in users)
                    role.UsersInRole.Add(_unitOfWork.UserRepository.GetById(u));
                _unitOfWork.RoleRepository.AttachStubs(role.UsersInRole.ToArray());

                foreach (var user in _unitOfWork.UserRepository.Get())
                {
                    if (user.UserRoles.Any(x => x.Id == role.Id))
                        user.UserRoles.Remove(user.UserRoles.First(x => x.Id == role.Id));
                }
                foreach (var prop in r.GetType().GetProperties())
                {
                    prop.SetValue(r, role.GetType().GetProperty(prop.Name).GetValue(role));
                }

                _unitOfWork.RoleRepository.Update(r);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs UpdateUser(User user, IEnumerable<int> roles))" + ex.Message);
                throw;
            }
        }
        
        public bool InRoles(string roles, User user)
        {
            try
            {
                if (user == null)
                {
                    throw new NullReferenceException("user");
                }
                if (string.IsNullOrWhiteSpace(roles))
                    return false;
                var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                return rolesArray.Select(role => _unitOfWork.UserRepository.GetById(user.Id).UserRoles.
                       Any(p => String.Compare(p.NameRole, role, StringComparison.OrdinalIgnoreCase) == 0)).
                       Any(hasRole => hasRole);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in AuthentificationService.cs InRoles(string roles, User user))" + ex.Message);
                throw;
            }
        }
  
    }
}

