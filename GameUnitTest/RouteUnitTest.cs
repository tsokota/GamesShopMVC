using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1;


namespace GameUnitTest
{
    [TestClass]
    public class RouteUnitTest
    {
        private RouteData GenarateRouteData(string URL)
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(URL);

            return routes.GetRouteData(httpContextMock.Object);
        }

        [TestMethod]
        public void DefaultRoute()
        {
            RouteData routeData = GenarateRouteData("~/");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("AllGames", routeData.Values["action"]);
        }

        // ~/games
        [TestMethod]
        public void AllGamesRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/games");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("AllGames", routeData.Values["action"]);
        }

        // ~/en/games/new
        [TestMethod]
        public void NewGamesTest()
        {
            RouteData routeData = GenarateRouteData("~/en/games/new");

            Assert.AreEqual("new", routeData.Values["action"]);
            Assert.AreEqual("Game", routeData.Values["controller"]);
        }

        // ~/en/games/update
        [TestMethod]
        public void UpdateGameActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/en/games/update");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("update", routeData.Values["action"]);
        }

        // ~/en/games/{gamekey}
        [TestMethod]
        public void GameDetailsRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/game/001");

            Assert.AreEqual("GameDetails", routeData.Values["action"]);
            Assert.AreEqual("001", routeData.Values["gamekey"]);
            Assert.AreEqual("Game", routeData.Values["controller"]);
        }

        // ~/en/games/remove
        [TestMethod]
        public void RemoveGameActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/en/games/remove");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("remove", routeData.Values["action"]);
        }

        // ~/en/game/{gamekey}/newcomment
        [TestMethod]
        public void NewCommentActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/en/game/001/newcomment");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("001", routeData.Values["gamekey"]);
            Assert.AreEqual("newcomment", routeData.Values["action"]);
        }

        // ~/en/game/{gamekey}/comments
        [TestMethod]
        public void AllCommentsActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/en/game/001/comments");

            Assert.AreEqual("comment", routeData.Values["controller"]);
            Assert.AreEqual("001", routeData.Values["gamekey"]);
            Assert.AreEqual("comments", routeData.Values["action"]);
        }

        // ~/en/game/{gamekey}/download
        [TestMethod]
        public void GameDownloadActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/game/001/download");

            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("001", routeData.Values["gamekey"]);
            Assert.AreEqual("download", routeData.Values["action"]);
        }

        // ~/en/publisher/new
        [TestMethod]
        public void NewPublisherActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/publisher/new");

            Assert.AreEqual("Publisher", routeData.Values["controller"]);
            Assert.AreEqual("new", routeData.Values["action"]);
        }

        // ~/en/publisher/}{companyName}
        [TestMethod]
        public void PublisherDetailsActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/publisher/NoPublisher");

            Assert.AreEqual("Publisher", routeData.Values["controller"]);
            Assert.AreEqual("NoPublisher", routeData.Values["CompanyName"]);
            Assert.AreEqual("PublisherDetails", routeData.Values["action"]);
        }

        // ~/en/game/{gamekey}/buy
        [TestMethod]
        public void GameBuyActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/game/001/buy");

            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("001", routeData.Values["gamekey"]);
            Assert.AreEqual("Buy", routeData.Values["action"]);
        }

        // ~/en/orders/history
        [TestMethod]
        public void OrdersHistoryActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/orders/history");

            Assert.AreEqual("Order", routeData.Values["controller"]);
            Assert.AreEqual("history", routeData.Values["action"]);
        }

        // ~/en/orders
        [TestMethod]
        public void AllOrdersActionRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/en/orders/");

            Assert.AreEqual("Order", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        //[TestMethod]
        //public void AllCommentsActionRoute_Test()
        //{
        //    RouteData routeData = GenarateRouteData("~/game/001/comments");

        //    Assert.AreEqual("comment", routeData.Values["controller"]);
        //    Assert.AreEqual("comments", routeData.Values["action"]);
        //}

        //[TestMethod]
        //public void DownloadActionRoute()
        //{
        //    RouteData routeData = GenarateRouteData("~/game/001/downloads");

        //    Assert.IsNotNull(routeData);
        //    Assert.AreEqual("Game", routeData.Values["controller"]);
        //    Assert.AreEqual("downloads", routeData.Values["action"]);
        //}

        //[TestMethod]
        //public void PublisherDetailsActionRoute()
        //{
        //    RouteData routeData = GenarateRouteData("~/publisher/CompanyName");

        //    Assert.IsNotNull(routeData);
        //    Assert.AreEqual("Publisher", routeData.Values["controller"]);
        //    Assert.AreEqual("PublisherDetails", routeData.Values["action"]);
        //}

        //[TestMethod]
        //public void NewPublisherActionRoute()
        //{
        //    RouteData routeData = GenarateRouteData("~/publisher/new");

        //    Assert.IsNotNull(routeData);
        //    Assert.AreEqual("Publisher", routeData.Values["controller"]);
        //    Assert.AreEqual("new", routeData.Values["action"]);
        //}

        [TestMethod]
        public void BusketActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/busket");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Order", routeData.Values["controller"]);
            Assert.AreEqual("GoToBusket", routeData.Values["action"]);
        }


        //[TestMethod]
        //public void AddToBusketActionRoute()
        //{
        //    RouteData routeData = GenarateRouteData("~/game/001/buy");

        //    Assert.IsNotNull(routeData);
        //    Assert.AreEqual("buy", routeData.Values["action"]);
        //    Assert.AreEqual("Game", routeData.Values["controller"]);
        //}
    }
}
