using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IGameService
    {
        void New(Game game);
        void Update(Game game);
        bool DeleteGame(string gamekey);
        IEnumerable<Game> AllGames();
        Game GameDetails(string gamekey);
        void Dispose();
        FileInfo downloads(string gamekey);
        Game GetGameByKey(string key);
    }
}
