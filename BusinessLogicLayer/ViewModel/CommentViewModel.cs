using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.ViewModel
{
    public class CommentViewModel
    {
       public Comment Comment { get; set; }

       public Comment ParentComment { get; set; }

       public string GameKey { get; set; }

       public string QuoteTag{get;set;}

       public List<Comment> CommentList { get; set; }
    }
}
