using AmadeusAPI.Controllers;
using AmadeusAPI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace AmadeusAPI.Tests
{
    /// <summary>
    /// Summary description for AirlineControllerTest
    /// </summary>
    [TestFixture]
    public class AirlineControllerTest
    {

        private AirlineController controller;
        
        #region Init
       
        [SetUp]
        public void SetupUnitTest()
        {
            controller = new AirlineController()
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };
        }

        [TearDown]
        public void TearDownUnitTest()
        {
            controller = null;
        }
        #endregion


        #region BadRequest

        [Test]
        public void SearchAirline_BadRequestWithEmpty_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "source or destination can not null or empty.",
            };
            
            //Act
            var actualResult = controller.SearchAirline(new SearchReq() { source = "", destination = "" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void SearchAirline_BadRequestWithNull_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "source or destination can not null or empty.",
            };
            
            //Act
            var actualResult = controller.SearchAirline(null);
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void SearchAirline_BadRequestWith_NotExistRoute_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "source or destination not match",
            };

            //Act
            var actualResult = controller.SearchAirline(new SearchReq() { source="A", destination="Z"});
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }


        [Test]
        public void ShortestPath_BadRequestWithEmpty_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "source or destination can not null or empty.",
            };

            //Act
            var actualResult = controller.ShortestPath(new SearchReq() { source = "", destination = "" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void ShortestPath_BadRequestWithNull_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "source or destination can not null or empty.",
            };

            //Act
            var actualResult = controller.ShortestPath(null);
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void SearchByRoutePath_BadRequestWithEmpty_ShouldReturn400()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 400,
                MessageDes = "routePath can not null or empty.",
            };

            //Act
            var actualResult = controller.SearchByRoutePath("");
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, (int)HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }


        #endregion


        #region Success

        [Test]
        public void SearchAirline_RequestWith_SourceAndDestination_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new List<ShortestResponse>() {
                new ShortestResponse(){
                    Routepath ="A-B-C-G-H-I-D",
                },
                new ShortestResponse(){
                    Routepath ="A-B-E-C-G-H-I-D",
                },
                new ShortestResponse(){
                    Routepath ="A-C-G-H-I-D",                    
                }
            };

            //Act
            var actualResult = controller.SearchAirline(new SearchReq() { source = "A", destination = "D" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as List<ShortestResponse>;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse[0].Routepath, actualResponse[0].Routepath);
            Assert.NotNull(actualResponse[0].Stations);
            Assert.IsTrue(actualResponse[0].Stations.Count > 0);

        }

        [Test]
        public void SearchAirline_RequestWith_Same_SourceAndDestination_ShouldReturn200()
        {
            
            //Arrange	
            var expectedModelResponse = new List<ShortestResponse>() {
                new ShortestResponse(){
                    Routepath ="A",
                    Cost = 0  //Note API can not support same soure and destination.
                }
            };

            //Act
            var actualResult = controller.SearchAirline(new SearchReq() { source = "A", destination = "A" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as List<ShortestResponse>;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse[0].Routepath, actualResponse[0].Routepath);
            Assert.NotNull(actualResponse[0].Stations);
            Assert.IsTrue(actualResponse[0].Stations.Count == 1);

        }

        [Test]
        public void ShortestPath_RequestWith_SourceAndDestination_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new ShortestResponse() {
               Routepath = "A-B-C",
               Cost = 110
            };

            //Act
            var actualResult = controller.ShortestPath(new SearchReq() { source = "A", destination = "C" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as ShortestResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Routepath, actualResponse.Routepath);
            Assert.NotNull(actualResponse.Stations);
            Assert.IsTrue(actualResponse.Stations.Count > 0);

        }

        [Test]
        public void ShortestPath_RequestWith_Same_SourceAndDestination_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new ShortestResponse()
            {
                Routepath = "A",
                Cost = 0 //cost must to 0
            };

            //Act
            var actualResult = controller.ShortestPath(new SearchReq() { source = "A", destination = "A" });
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as ShortestResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Routepath, actualResponse.Routepath);
            Assert.NotNull(actualResponse.Stations);
            Assert.IsTrue(actualResponse.Stations.Count== 1);

        }


        [Test]
        public void SearchByRoutePath_RequestWith_RoutePath_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new ShortestResponse()
            {
                Routepath = "A-B-E-C-G-H-I-D",
            };

            //Act
            var actualResult = controller.SearchByRoutePath("A-B-E-C-G-H-I-D");
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as ShortestResponse;
            Assert.NotNull(actualResponse);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Routepath, actualResponse.Routepath);
            Assert.NotNull(actualResponse.Stations);
            Assert.IsTrue(actualResponse.Stations.Count > 0);

        }

        [Test]
        public void SearchByRoutePath_RequestWith_IncorrectRoutePath_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 200,
                MessageDes = "Can not found this route path. Please try any route path again.",
            };

            //Act
            var actualResult = controller.SearchByRoutePath("A-B-E-N-G-H-I-D");
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert           
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, actualResponse.Messagecode);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void SearchByRoutePath_RequestWith_NotRoutePath_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new SearchResponse
            {
                Messagecode = 200,
                MessageDes = "Can not found this route path. Please try any route path again.",
            };

            //Act
            var actualResult = controller.SearchByRoutePath("XXXXXXXX");
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as SearchResponse;
            Assert.NotNull(actualResponse);

            //Assert           
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.AreEqual(expectedModelResponse.Messagecode, actualResponse.Messagecode);
            Assert.AreEqual(expectedModelResponse.MessageDes, actualResponse.MessageDes);
        }

        [Test]
        public void GetAllRoutes_ShouldReturn200()
        {
            //Arrange	
            var expectedModelResponse = new List<Route>() {
                new Route("A","B",60),
                new Route("A","C",150),
            };
            
            //Act
            var actualResult = controller.GetAllRoutes();
            var objContent = actualResult.Content as ObjectContent;
            Assert.NotNull(objContent);
            var actualResponse = objContent.Value as List<Route>;
            Assert.NotNull(actualResponse);

            //Assert           
            Assert.AreEqual(HttpStatusCode.OK, actualResult.StatusCode);
            Assert.NotNull(actualResponse[0]);
            Assert.AreEqual(expectedModelResponse[0].From, actualResponse[0].From);
            Assert.AreEqual(expectedModelResponse[0].To, actualResponse[0].To);
            Assert.AreEqual(expectedModelResponse[0].Cost, actualResponse[0].Cost);
            Assert.NotNull(actualResponse[1]);
            Assert.AreEqual(expectedModelResponse[1].From, actualResponse[1].From);
            Assert.AreEqual(expectedModelResponse[1].To, actualResponse[1].To);
            Assert.AreEqual(expectedModelResponse[1].Cost, actualResponse[1].Cost);
        }
        
        #endregion

    }
}
