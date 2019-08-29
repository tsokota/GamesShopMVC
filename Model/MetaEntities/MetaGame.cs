using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Model.MetaEntities
{
    class MetaGame
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "KeyGame", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "Key")]
        [StringLength(100, MinimumLength = 3)]
        public string Key { get; set; }

        [Display(Name = "NameGame", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "min -3 , max - 50 symbols")]
        public string Name { get; set; }

        [Display(Name = "Descriptions", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
       ErrorMessageResourceName = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "min- 3, max - 2000 symbols")]
        public string Description { get; set; }

        [Display(Name = "IsRemove", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Discontinued", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "Discontinued")]
        [UIHint("Boolean")]
        public bool Discontinued { get; set; }

        [Display(Name = "Prices", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "Price")]
        public double Price { get; set; }

        [Display(Name = "GameProductions", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "GameProduction")]
        [DataType(DataType.DateTime)]
        public DateTime GameProduction { get; set; }

        [Display(Name = "UnitsInStocks", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "UnitsInStock")]
        public int UnitsInStock { get; set; }

        public string Picture { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Platform> Platforms { get; set; }

    }
}
