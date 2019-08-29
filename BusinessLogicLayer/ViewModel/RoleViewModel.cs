using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel
{
    public class RoleViewModel
    {
        public Role Role { get; set; }

        public ICollection<User> Users { set; get; }

        public List<int> SelectedUsers { set; get; }
    }
}
