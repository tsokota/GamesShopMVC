using System.Collections.Generic;
namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class GenreDTO
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        public ICollection<GenreDTO> SubGenres { get; set; }
    }
}