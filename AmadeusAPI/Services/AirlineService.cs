using AmadeusAPI.Controllers;
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
        List<string> GetAllPaths(SearchReq request);

        Route GetShortestPath(SearchReq request);

    }
    public class AirlineService : IAirlineService
    {
        private static readonly Type CurrentClass = typeof(AirlineService);
     
        public List<string> GetAllPaths(SearchReq request)
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
                return graph.Result.ToList();

            }
            catch (Exception ex)
            {
                AirlineLogManager.Error(null, CurrentClass, currentMethod, ex);                
            }
            return null;
        }

        public Route GetShortestPath(SearchReq request)
        {
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            try
            {
                Dijkstra dijkstra = new Dijkstra();
                dijkstra.ShortestPath(request.source, request.destination);
                return dijkstra.Result ;
            }
            catch (Exception ex)
            {
                AirlineLogManager.Error(null, CurrentClass, currentMethod, ex);

            }
            return null;
        }
    }
}