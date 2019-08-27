using AmadeusAPI.Filters;
using AmadeusAPI.Helpers;
using AmadeusAPI.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;


namespace AmadeusAPI.Controllers
{
    public class AirlineController : ApiController
    {
        Type currentClass = typeof(AirlineController);

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpPost]
        [ActionName("SearchAirline")]      
        public HttpResponseMessage SearchAirline(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Format("source: {0} to destination: {1}", request.source , request.destination), currentClass, currentMethod);

            if (string.IsNullOrEmpty(request.source) || string.IsNullOrEmpty(request.source))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { messagecode = (int)HttpStatusCode.BadRequest, messagedes = "source or destination can not null or empty." });
            }

            Regex r = new Regex(@"^[A-I]{1}$");
            if (!r.IsMatch(request.source) || !r.IsMatch(request.source))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { messagecode = (int)HttpStatusCode.BadRequest, messagedes = "source or destination not match" });
            }

            IAirlineService airlineService = new AirlineService();
            var  result = airlineService.GetAllPaths(request);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpPost]
        [ActionName("ShortestPath")]
        public HttpResponseMessage ShortestPath(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Format("source: {0} to destination: {1}", request.source, request.destination), currentClass, currentMethod);

            if (string.IsNullOrEmpty(request.source) || string.IsNullOrEmpty(request.source))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { messagecode = (int)HttpStatusCode.BadRequest, messagedes = "source or destination can not null or empty." });
            }

            Regex r = new Regex(@"^[A-I]{1}$");
            if (!r.IsMatch(request.source) || !r.IsMatch(request.source)){
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { messagecode = (int)HttpStatusCode.BadRequest, messagedes = "source or destination not match" });
            }

            IAirlineService airlineService = new AirlineService();
            var result = airlineService.GetShortestPath(request);

            return Request.CreateResponse(HttpStatusCode.OK, result);           
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpGet]
        [ActionName("SearchByRoutePath")]
        public HttpResponseMessage SearchByRoutePath(string routePath)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Format("key : {0}", routePath), currentClass, currentMethod);

            if (string.IsNullOrEmpty(routePath))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new SearchResponse { messagecode = (int)HttpStatusCode.BadRequest, messagedes = "routePath can not null or empty." });
            }

            //Regex r = new Regex(@"^[A-I]{1}$");
            //if (!r.IsMatch(request.source) || !r.IsMatch(request.source))
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, "source or destination not match");
            //}

            var path = routePath.Split('-').ToList() ;       
            IAirlineService airlineService = new AirlineService();
            var result = airlineService.GetAllPaths(new SearchReq { source = path.FirstOrDefault(), destination =path.LastOrDefault()});

            if (!result.Contains(routePath)) {
                return Request.CreateResponse(HttpStatusCode.OK, new SearchResponse { messagecode = (int)HttpStatusCode.OK ,messagedes= "Can not found this route path. Please try any route path again." });
            }
            return Request.CreateResponse(HttpStatusCode.OK, routePath);
        }
    }

   
}
