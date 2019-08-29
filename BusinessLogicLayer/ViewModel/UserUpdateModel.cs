using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel
{
    public class UserUpdateModel
    {
        public User User { get; set; }

        public ICollection<Role> Roles { set; get; }

        public List<int> SelectedRoles { set; get; }
    }
}
