using System.Collections.Generic;

namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public int? GameId { get; set; }

        public string GameKey { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}