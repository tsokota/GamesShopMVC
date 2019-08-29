using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [ExceptionControllerAttribute]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
       
       
        private readonly IAuthenticationService _service;
     
        public AdminController(IAuthenticationService service, ILogger logger):base(logger)
        {
            _service = service;
        }
        #region User
        /// <summary>
        /// view with authorized users 
        /// </summary>
        /// <returns>return list of all registered users</returns>
        public ActionResult AuthUsers()
        {
           
                var authUsers = _service.GetUsers();
                return View(authUsers);
            
        }


        public ActionResult UserDetails(int userId = 0)
        {
          
                var authUsers = _service.GetUser(userId);
                return View(authUsers);
           
        }


        [HttpGet]
        public ActionResult UpdateUser(int userId = 0)
        {
            
                UserUpdateModel model = new UserUpdateModel();
                model.User = _service.GetUser(userId);
                model.Roles = _service.GetRoles().ToList();
                model.SelectedRoles = model.User.UserRoles.Select(x => x.Id).ToList();
                return View(model);
          
        }

        [HttpPost]
        public ActionResult UpdateUser(UserUpdateModel model)
        {
           
                model.Roles = _service.GetRoles().ToList();
                if (!ModelState.IsValid)
                {

                    return View(model);
                }
                if (ValidationUserData(model) != null)
                    return View(model);
                _service.UpdateUser(model.User, model.SelectedRoles);
                return RedirectToAction("AuthUsers", "Admin");


           
        }

        public UserUpdateModel ValidationUserData(UserUpdateModel model)
        {
            User user = _service.GetUser(model.User.Id);
            if (user.Email != model.User.Email && !_service.EmailExist(model.User.Email))
            {
                ModelState.AddModelError("User.Email", "This Email address already exists");
                return model;
            }
            if (user.Login != model.User.Login && _service.GetUser(model.User.Login) != null)
            {

                ModelState.AddModelError("User.Login", "This Login already exists");
                return model;
            }
            return null;              
        }


        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
          
                return View(_service.GetUser(userId));
        }

        [HttpPost]
        public ActionResult DeleteUser(User user)
        {
             if (user == null)
                {
                    Logger.Error("user is null DeleteUser(User) AdminController.cs");
                    throw new NullReferenceException("User");
                }
                _service.DeleteUser(user);
                return RedirectToAction("AuthUsers", "Admin");
           
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
           
                UserUpdateModel model = new UserUpdateModel();
                model.Roles = _service.GetRoles().ToList();
                model.SelectedRoles = new List<int>();
                return View(model);
            
        }

        [HttpPost]
        public ActionResult CreateUser(UserUpdateModel model)
        {
            model.Roles = _service.GetRoles().ToList();
            if (!ModelState.IsValid)
            {

                return View(model);

            }
            if (ValidationUserData(model) != null)
                return View(model);
            if (model.SelectedRoles != null && model.SelectedRoles.Count > 0)
            {
                model.User.UserRoles = new List<Role>();
                model.SelectedRoles.ForEach(q => model.User.UserRoles.Add(new Role { Id = q }));
            }
            _service.CreateUser(model.User);
            return RedirectToAction("AuthUsers", "Admin");
        }
        #endregion

        #region Role
        public ActionResult AuthRoles()
        {
           
                IEnumerable<Role> roles = _service.GetRoles();
                return View(roles);
           
        }


        [HttpGet]
        public ActionResult UpdateRole(int roleId)
        {
            
                RoleViewModel model = new RoleViewModel();
                model.Role = _service.GetRole(roleId);
                model.Users = _service.GetUsers().ToList();
                model.SelectedUsers = new List<int>();
                model.Role.UsersInRole.ToList().ForEach(q => model.SelectedUsers.Add(q.Id));
                return View(model);
           
        }

        [HttpPost]
        public ActionResult UpdateRole(RoleViewModel model)
        {
           
                if (!ModelState.IsValid)
                {
                    model.Users = _service.GetUsers().ToList();
                    return View(model);

                }
                Role role = _service.GetRole(model.Role.Id);
                if (role.NameRole != model.Role.NameRole && !_service.RoleExist(model.Role.NameRole))
                {
                    model.Users = _service.GetUsers().ToList();
                    ModelState.AddModelError("Role.RoleName", "This Role  already exists");
                    return View(model);
                }
                _service.UpdateRole(model.Role, model.SelectedUsers);
                return RedirectToAction("AuthRoles", "Admin");
            
        }

        [HttpGet]
        public ActionResult DeleteRole(int roleId = 0)
        {
           
                return View(_service.GetRole(roleId));
           
        }

        [HttpPost]
        public ActionResult DeleteRole(Role role)
        {
           
                if (role == null)
                {
                    Logger.Error("null role DeleteRole AdminController.cs");
                    throw new ArgumentNullException("role");
                }
                _service.DeleteRole(role);
                return RedirectToAction("AuthRoles", "Admin");
          
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
           
                RoleViewModel model = new RoleViewModel();
                model.Users = _service.GetUsers().ToList();
                model.SelectedUsers = new List<int>();
                return View(model);

        }

        [HttpPost]
        public ActionResult CreateRole(RoleViewModel model)
        {
            model.Users = _service.GetUsers().ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_service.RoleExist(model.Role.NameRole))
            {
                ModelState.AddModelError("Role.RoleName", "This Name already exists");
                return View(model);
            }

            if (model.SelectedUsers != null && model.SelectedUsers.Count > 0)
            {
                model.Role.UsersInRole = new List<User>();
                model.SelectedUsers.ForEach(q => model.Role.UsersInRole.Add(new User { Id = q }));
            }
            _service.CreateRole(model.Role);
            return RedirectToAction("AuthRoles", "Admin");
        }

        public ActionResult RoleDetails(int roleId = 0)
        {
             return View(_service.GetRole(roleId));
           
        }
        #endregion


    }
}
