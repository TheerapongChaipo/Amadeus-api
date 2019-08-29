using AmadeusAPI.Filters;
using AmadeusAPI.Helpers;
using AmadeusAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;


namespace AmadeusAPI.Controllers
{
    [RoutePrefix("airline")]
    public class AirlineController : ApiController
    {
        Type currentClass = typeof(AirlineController);

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpPost]
        [Route("search/airline")]       
        public HttpResponseMessage SearchAirline(SearchReq request)
        {            
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
          
            if (request == null || string.IsNullOrEmpty(request.source) || string.IsNullOrEmpty(request.source))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { Messagecode = (int)HttpStatusCode.BadRequest, MessageDes = "source or destination can not null or empty." });
            }

            AirlineLogManager.Entering(string.Format("source: {0} to destination: {1}", request.source, request.destination), currentClass, currentMethod);

            Regex r = new Regex(@"^[A-I]{1}$");
            if (!r.IsMatch(request.source) || !r.IsMatch(request.destination))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { Messagecode = (int)HttpStatusCode.BadRequest, MessageDes = "source or destination not match" });
            }

            IAirlineService airlineService = new AirlineService();
            List<ShortestResponse> result = airlineService.GetAllPaths(request);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpPost]
        [Route("search/shortest")]
        public HttpResponseMessage ShortestPath(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();

            if (request == null || string.IsNullOrEmpty(request.source) || string.IsNullOrEmpty(request.source))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { Messagecode = (int)HttpStatusCode.BadRequest, MessageDes = "source or destination can not null or empty." });
            }

            AirlineLogManager.Entering(string.Format("source: {0} to destination: {1}", request.source, request.destination), currentClass, currentMethod);

            Regex r = new Regex(@"^[A-I]{1}$");
            if (!r.IsMatch(request.source) || !r.IsMatch(request.source)) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { Messagecode = (int)HttpStatusCode.BadRequest, MessageDes = "source or destination not match" });
            }

            IAirlineService airlineService = new AirlineService();
            ShortestResponse result = airlineService.GetShortestPath(request);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpGet]
        [Route("search/byroutepath")]
        public HttpResponseMessage SearchByRoutePath(string routePath)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Format("key : {0}", routePath), currentClass, currentMethod);

            if (string.IsNullOrEmpty(routePath))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { Messagecode = (int)HttpStatusCode.BadRequest, MessageDes = "routePath can not null or empty." });
            }

            var path = routePath.Split('-').ToList();
            IAirlineService airlineService = new AirlineService();
            var result = airlineService.GetAllPaths(new SearchReq { source = path.FirstOrDefault(), destination = path.LastOrDefault() });

            if (result.Any(x => x.Routepath == routePath)) {
                ShortestResponse ppp = result.Where(x => x.Routepath == routePath).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, ppp);
            }
            else {
                return Request.CreateResponse(HttpStatusCode.OK, new SearchResponse { Messagecode = (int)HttpStatusCode.OK, MessageDes = "Can not found this route path. Please try any route path again." });
            }
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpGet]
        [Route("routes")]
        public HttpResponseMessage GetAllRoutes()
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Empty, currentClass, currentMethod);
           
            IAirlineService airlineService = new AirlineService();
            var result = airlineService.GetAllRoutes();
            return Request.CreateResponse(HttpStatusCode.OK, result);         
        }
    }

   
}
