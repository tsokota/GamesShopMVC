using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web.UI;

namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class GameDTO
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public int? PublisherId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime Productioned { get; set; }

        public bool IsDeleted { get; set; }

        public string Picture { get; set; }

        public PublisherDTO Publisher { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }

        public ICollection<PlatformDTO> Platforms { get; set; }
    }
}