using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.MetaEntities
{
    class MetaPlatform
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NamePlatform", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
         ErrorMessageResourceName = "PlatformRequired")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }


        [Display(Name = "IsRemove", ResourceType = typeof(ModelRes))]
        [UIHint("Boolean")]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
         ErrorMessageResourceName = "isDeletedRequired")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Game> Games { get; set; }


    }
}
