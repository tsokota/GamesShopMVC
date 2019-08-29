using DAL;
using Model;
using Model.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Services
{
    public class GameService:IGameService
    {
        private UnitOfWork unitOfWork;
        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public GameService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public GameService()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public GameService(GenericRepository<Game> genericRepository, 
            GenericRepository<EntityView> viewRepository)
        {
            this.unitOfWork = new UnitOfWork();
            unitOfWork.GameRepository = genericRepository;
            unitOfWork.ViewRepository = viewRepository;
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }


        public void New(Game game)
        {
            try
            {
                if (game == null)
                {
                    throw new ArgumentNullException("null game data");
                }
                unitOfWork.GameRepository.Insert(game);
                unitOfWork.Save();
                logger.Debug("result succsess - added new game: {0}, {1}", game.Name, game.Key);
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: New" + "\n" + ex.Message);    
            }


        }

        public void Update(Game game)
        {
            try
            {
                if (game == null)
                {
                    logger.Error("null [game] parametr data");
                    throw new ArgumentNullException("null game data");
                }
                unitOfWork.GameRepository.Update(game);
                unitOfWork.Save();
                logger.Debug("result succsess - update game: {0}, key {1} ", game.Name, game.Key);
                logger.Debug("Action  -> Upadate have worked success!");
              
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: Update" + "\n" + ex.Message);
              
            }
        }

        public bool DeleteGame(string gamekey)
        {
            try
            {
                //unitOfWork.GameRepository.Delete(gamekey);
                Game game = this.GetGameByKey(gamekey);
                game.IsDeleted = true;

                unitOfWork.GameRepository.Update(game);

                unitOfWork.Save();
                logger.Debug("result succsess - delete game:  key {0} ", gamekey);
                logger.Debug("Action  -> DeleteGame have worked success!");

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: DeleteGame" + "\n" + ex.Message);
                return false;
            }
        }

        public IEnumerable<Game> AllGames()
        {
            try
            {
                var games = unitOfWork.GameRepository.Get(x => true);
                unitOfWork.Save();
                logger.Debug("result succsess - AllGames");
                logger.Debug("Action  -> have worked success!");

                return games;
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: AllGame" + "\n" + ex.Message);
                return null;
            }
        }

        public Game GameDetails(string gamekey)
        {
            try
            {
                var game = this.AllGames().Where(x => x.Key == gamekey).SingleOrDefault();
                    //unitOfWork.GameRepository.GetByID(gamekey);
                // add  new view
                unitOfWork.ViewRepository.Insert(new EntityView { DateView = DateTime.Now, TypeEntity = EntityType.Game, IdEntity = gamekey });
                logger.Debug("Action  -> GameDetails() have worked success!");
                return game;
              
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: GameDetails" + "\n" + ex.Message);
                return null;
               
            }
        }

        public FileInfo downloads(string gamekey)
        {
            try
            {
                Random r = new Random();
                FileInfo file = new FileInfo(r.Next(0, 10000).GetHashCode().ToString());
                if (file.Exists == false) //file not exist
                {
                    file.Create(); //create file
                }
                logger.Info("result succsess");
                return file;
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: downloads" + "\n" + ex.Message);
                return null;
            }
        }

        public Game GetGameByKey(string key)
        {
            if (key == null)
                throw new Exception("id is null");
            var game = unitOfWork.GameRepository.Get(a=>true).Where(x=>x.Key==key).SingleOrDefault();
            if (game == null)
                throw new Exception("game is bull");
            else return game;
        }
    }
}
