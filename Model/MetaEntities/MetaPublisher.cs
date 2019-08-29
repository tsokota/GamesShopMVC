using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.MetaEntities
{
    class MetaPublisher
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }


        [StringLength(40, MinimumLength = 3, ErrorMessage = "Length from 3 - 40 symbols")]

        [Display(Name = "CompanyName", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "CompanyNameRequired")]
        public string CompanyName { get; set; }

        [Display(Name = "IsRemove", ResourceType = typeof(ModelRes))]
        [UIHint("Boolean")]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Descriptions", ResourceType = typeof(ModelRes))]
        [DataType(DataType.MultilineText)]
        [MaxLength]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "DescriptionRequired")]
        public string Description { get; set; }

        [Display(Name = "HomePage", ResourceType = typeof(ModelRes))]
        [DataType(DataType.Url)]
        [MaxLength]
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
