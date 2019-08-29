using Model.MetaEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaPlatform))]
    public class Platform : IGenericModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Platform()
        {
            Games = new List<Game>();
        }
    }
}
