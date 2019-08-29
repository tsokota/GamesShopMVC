using System;
using System.ComponentModel.DataAnnotations;

namespace Model.MetaEntities
{
    public class MetaBanUser
    {
        public class BanUser
        {
            [Key]
            public int Id { get; set; }

            [Display(Name = "beginBan", ResourceType = typeof(ModelRes))]
            [Required(ErrorMessageResourceType = typeof(ModelRes),
            ErrorMessageResourceName = "beginBanError")]
            public DateTime BeginBan { get; set; }

            [Display(Name = "lastBan", ResourceType = typeof(ModelRes))]
            [Required(ErrorMessageResourceType = typeof(ModelRes),
            ErrorMessageResourceName = "lastBanError")]
            public DateTime LastBan { get; set; }

            [Display(Name = "reasonBan", ResourceType = typeof(ModelRes))]
            [Required(ErrorMessageResourceType = typeof(ModelRes),
            ErrorMessageResourceName = "reasonBanError")]
            public string ReasonBan { get; set; }
        }
    }
}
