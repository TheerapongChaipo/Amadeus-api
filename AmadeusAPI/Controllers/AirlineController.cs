﻿using AmadeusAPI.Filters;
using AmadeusAPI.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;


namespace AmadeusAPI.Controllers
{
   // [EnableCors("*", "*", "*")]
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
          
            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        [BasicAuthentication]
        [ThrottleFilter()]
        [HttpPost]
        [ActionName("ShortestPath")]
        public HttpResponseMessage ShortestPath(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            AirlineLogManager.Entering(string.Format("source: {0} to destination: {1}", request.source, request.destination), currentClass, currentMethod);

            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

    }

   
}
