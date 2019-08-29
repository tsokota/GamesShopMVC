using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Body { get; set; }

        public string AuthorName { get; set; }

        public string ParentName { get; set; }

        public DateTime DateComment { get; set; }
      
        public virtual Game Game { get; set; }

        

        // For realisation comment on comment
        public virtual ICollection<Comment> Comments { get; set; }


        public Comment()
        {
            DateComment = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}
