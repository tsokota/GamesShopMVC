using Model.MetaEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaRole))]
    public class Role : IGenericModel
    {
        public int Id { get; set; }

        public string NameRole { get; set; }

        public virtual ICollection<User> UsersInRole { get; set; }
    }
}
