using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Model.MetaEntities
{
    class MetaComment
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "CommentBody", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "Body")]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "min -3 , max - 2000 symbols")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "AuthorNameComment", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "AuthorName")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "min -3 , max - 20 symbols")]
        public string AuthorName { get; set; }

        public string ParentName { get; set; }

        [Display(Name = "DateComments", ResourceType = typeof(ModelRes))]
       
        [DataType(DataType.DateTime)]
        public DateTime DateComment { get; set; }

        public virtual Game Game { get; set; }

        // For realisation comment on comment
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
