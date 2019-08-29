using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Model.MetaEntities
{
    class MetaRole
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "RoleName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "NameRoleRequired")]
        [StringLength(100, MinimumLength = 3)]
        public string NameRole { get; set; }

        public virtual ICollection<User> UsersInRole { get; set; }
    }
}
