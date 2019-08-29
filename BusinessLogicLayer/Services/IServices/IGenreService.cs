using System.Collections.Generic;
using Model.Entities;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IGenreService : IGenericService<Genre>
    {
        Genre Get(int id, int northWind);

        void SetForGame(string gameKey, IEnumerable<string> genreNames);
    }
}
