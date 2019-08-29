using Model.MetaEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaPublisher))]
    public class Publisher : IGenericModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public bool IsDeleted { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public int NorthWindId { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
