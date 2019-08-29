using Model.MetaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaComment))]
    public class Comment : IGenericModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string AuthorName { get; set; }

        public string ParentName { get; set; }

        public DateTime DateComment { get; set; }

        public int ParentId { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }



        // For realisation comment on comment
        public virtual ICollection<Comment> Comments { get; set; }


        public Comment()
        {
            DateComment = DateTime.Now;
            Comments = new List<Comment>();
            ParentName = null;
        }
    }
}
