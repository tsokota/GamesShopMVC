using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Services;
using Model.Reporting;
using System.IO;

namespace GameUnitTest
{
    [TestClass]
    public class GameServiceTest
    {
        private Mock<GenericRepository<Game>> gameRepo;
        private Mock<GenericRepository<EntityView>> viewRepo;

        private GameService gameService;

        [TestInitialize]
        public void Init()
        {
            List<Game> games = new List<Game>();
            #region InitGames
            Game game = new Game();

            game.Key = "001";
            game.Name = "Warcraft III: Reign of Chaos";
            game.Description = "Warcraft III: Reign of Chaos is a high fantasy real-time strategy video game released " +
                "by Blizzard Entertainment on July 3, 2002 in the U.S. It is the second sequel to Warcraft: Orcs & Humans, " +
                "and it is the third game set in the Warcraft fictional Universe. An expansion pack, The Frozen Throne, " +
                "was released on July 1, 2003.";

            games.Add(game);

            game = new Game();
            game.Key = "002";
            game.Name = "Fallout 3";
            game.Description = "Fallout 3 takes place in the year 2277, 36 years after the setting of " +
                "Fallout 2 and 200 years after the nuclear apocalypse that devastated the game's world " +
                "in a future where international conflicts between the United States and China culminated " +
                "in a Sino-American war in 2077, due to the scarcity of petroleum reserves that ran the economies " +
                "of both countries. The player character is an inhabitant of Vault 101, a survival shelter designed " +
                "to protect up to 1,000 humans from the nuclear fallout. When the player character's father disappears " +
                "under mysterious circumstances, the Overseer, or the leader of the vault, initiates martial law, " +
                "and sends security forces after the player, who is forced to escape from the Vault and journey into " +
                "the ruins of Washington, D.C. to track him down. Along the way the player is assisted by a number of " +
                "human survivors and must battle a myriad of enemies that inhabit the area now known as the \"Capital Wasteland\".";
            games.Add(game);

            game = new Game();
            game.Key = "003";
            game.Name = "Uncharted 3: Drake's Deception";
            game.Description = "Uncharted 3: Drake's Deception is a 2011 action-adventure third-person shooter " +
                "platform video game, the third game in the Uncharted series, developed by Naughty Dog, " +
                "with a story written by script-writer Amy Hennig. It is the sequel to one of the most critically " +
                "acclaimed video games of 2009, Uncharted 2: Among Thieves.[1] The game was released by Sony Computer" +
                "Entertainment for the PlayStation 3, in North America on November 1, 2011, Europe on November 2, 2011, " +
                "and Australia on November 3, 2011. A Game of the Year Edition, containing all additional content that was " +
                "a part of the Fortune Hunters' Club deal, was released on September 19, 2012, for Europe.";
            games.Add(game);



            #endregion

            gameRepo = new Mock<GenericRepository<Game>>();
            gameRepo.Setup(x => x.Get(a => true, null, "")).Returns(games);

            viewRepo = new Mock<GenericRepository<EntityView>>();
            viewRepo.Setup(x => x.Get(a => true, null, "")).Returns(new List<EntityView>());

            gameService = new GameService(gameRepo.Object, viewRepo.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ExceptionIfNullGameRepository()
        {
            GameStoreContext context = null;
            GenericRepository<Game> repo = new GenericRepository<Game>(context);
        }

        [TestMethod]
        public void GetAllGames()
        {
            List<Game> list = gameRepo.Object.Get(a => true, null, "").ToList<Game>();
            Assert.AreEqual(3, list.Count);

        }

        [TestMethod]
        public void GetGame()
        {
            Game game = gameService.GameDetails("001");

            Assert.IsNotNull(game);
            Assert.AreEqual("Warcraft III: Reign of Chaos", game.Name);
        }

        [TestMethod]
        public void Update()
        {
            Game game = gameService.GetGameByKey("001");
            game.Name = "New Name!!!";

            gameService.Update(game);

            Assert.AreEqual(game, gameService.GetGameByKey("001"));
        }

        [TestMethod]
        public void Delete()
        {
            //Act
            Game game1 = gameService.GetGameByKey("001");
            Game game2 = gameService.GetGameByKey("002");
            Game game3 = gameService.GetGameByKey("003");
            bool IsGame1Deleted = gameService.DeleteGame("001");
            bool IsGame3Deleted = gameService.DeleteGame("003");
            
            //Assert
            Assert.AreEqual(true, game1.IsDeleted);
            Assert.AreEqual(true, game3.IsDeleted);
            Assert.AreEqual(false, game2.IsDeleted);
        }

        [TestMethod]
        public void Download()
        {
            FileInfo file = gameService.downloads("001");

            Assert.IsNotNull(file);
        }
    }
}
