﻿using AmadeusAPI.Controllers;
using AmadeusAPI.Helpers;
using AmadeusAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AmadeusAPI.Services
{
    public interface IAirlineService
    {
        List<ShortestResponse> GetAllPaths(SearchReq request);

        ShortestResponse GetShortestPath(SearchReq request);

        List<Route> GetAllRoutes();

    }
    public class AirlineService : IAirlineService
    {
        private static readonly Type CurrentClass = typeof(AirlineService);
     
        public List<ShortestResponse> GetAllPaths(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            try
            {
                DepthFirstTraversal graph = new DepthFirstTraversal(9);
                graph.addEdge(0, 1);
                graph.addEdge(0, 2);
                graph.addEdge(1, 2);
                graph.addEdge(1, 4);
                graph.addEdge(2, 1);
                graph.addEdge(2, 6);
                graph.addEdge(3, 8);
                graph.addEdge(4, 0);
                graph.addEdge(4, 2);
                graph.addEdge(5, 0);
                graph.addEdge(5, 6);
                graph.addEdge(6, 5);
                graph.addEdge(6, 7);
                graph.addEdge(7, 8);
                graph.addEdge(8, 2);
                graph.addEdge(8, 3);
                var source = graph.datacode.FirstOrDefault(x => x.Key == request.source).Value;
                var destination = graph.datacode.FirstOrDefault(x => x.Key == request.destination).Value;
                graph.printAllPaths(source, destination);
                return graph.ShortestResponses;

            }
            catch (Exception ex)
            {
                AirlineLogManager.Error(null, CurrentClass, currentMethod, ex);                
            }
            return null;
        }

        public ShortestResponse GetShortestPath(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            try
            {
                Dijkstra dijkstra = new Dijkstra();
                dijkstra.ShortestPath(request.source, request.destination);
                var response = new ShortestResponse();
                response.Stations = new List<Stations>();
                response.Stations = dijkstra.stations;
                response.Routepath = dijkstra.routpath;
                response.Cost = dijkstra.Cost;
                return response;
            }
            catch (Exception ex)
            {
                AirlineLogManager.Error(null, CurrentClass, currentMethod, ex);

            }
            return null;
        }

        public List<Route> GetAllRoutes()
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            try
            {
                Dijkstra dijkstra = new Dijkstra();
                dijkstra.initGraph();
                return dijkstra.routes;
            }
            catch (Exception ex)
            {
                AirlineLogManager.Error(null, CurrentClass, currentMethod, ex);
            }
            return null;
        }
    }
}