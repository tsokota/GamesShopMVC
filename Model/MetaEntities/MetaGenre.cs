using Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Model.MetaEntities
{
    class MetaGenre
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "GenreNameRequired")]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }

        public Genre ParentGenre { get; set; }


        [Display(Name = "IsRemove", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "IsDeletedRequired")]
        [UIHint("Boolean")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Genre> SubGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
