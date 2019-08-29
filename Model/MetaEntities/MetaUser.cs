using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Model.MetaEntities
{
    class MetaUser
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Login", ResourceType = typeof(ModelRes))]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "LoginRequired")]
        public string Login { get; set; }


        [Display(Name = "Password", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }

        [UIHint("Boolean")]
        public bool IsPersistent { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ModelRes))]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Not correct")]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
         ErrorMessageResourceName = "EmailRequired")]
        public string Email { get; set; }


        public string AvatarPath { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthdate { get; set; }

        [DataType(DataType.DateTime)]
        public DataType? AddedDate { get; set; }


        public virtual ICollection<Role> UserRoles { get; set; }
    }
}
