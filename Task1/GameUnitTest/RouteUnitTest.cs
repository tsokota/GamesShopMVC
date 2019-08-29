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
            RouteCollection routes = new RouteCollection();
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
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void AllGamesRouteTest()
        {
            RouteData routeData = GenarateRouteData("~/games");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("AllGames", routeData.Values["action"]);
        }

        [TestMethod]
        public void NewGameActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/games/new");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("new", routeData.Values["action"]);
        }

        [TestMethod]
        public void UpdateGameActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/games/update");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("update", routeData.Values["action"]);
        }

        [TestMethod]
        public void RemoveGameActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/games/remove");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("remove", routeData.Values["action"]);
        }

        [TestMethod]
        public void GameDetailsActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/game/001");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("GameDetails", routeData.Values["action"]);
        }

        [TestMethod]
        public void NewCommentActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/game/001/newcomment");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("newcomment", routeData.Values["action"]);
        }

        [TestMethod]
        public void AllCommentsActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/game/001/comments");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("comments", routeData.Values["action"]);
        }

        [TestMethod]
        public void DownloadActionRoute()
        {
            RouteData routeData = GenarateRouteData("~/game/001/downloads");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"]);
            Assert.AreEqual("downloads", routeData.Values["action"]);
        }
    }
}
