using System;
namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class GameDTOeasy
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public int? PublisherId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime Producted { get; set; }

        public bool IsDeleted { get; set; }

        public string Picture { get; set; }

    }
}