using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AmadeusAPI.Models
{
    public class Dijkstra
    {
        //private readonly Route _result = new Route>();
        public Route Result { get; set; }

        public Dictionary<string, Node> nodeDict = new Dictionary<string, Node>();
        public List<Route> routes { get; set; }

        public Dijkstra()
        {
            routes = new List<Route>();
        }

        static HashSet<string> unvisited = new HashSet<string>();

        public void ShortestPath(string startNode, string destNode)
        {
            try
            {
                initGraph();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            nodeDict[startNode].Value = 0;
            var queue = new PrioQueue();
            queue.AddNodeWithPriority(nodeDict[startNode]);
            CheckNode(queue, destNode);
            PrintShortestPath(startNode, destNode);
        }

        private void initGraph()
        {
            //Load rount from File , must move to DB
            var apPath = HttpContext.Current.Server.MapPath(@"~/Models/graph2.txt");
            if (!File.Exists(apPath))
            {
                throw new FileNotFoundException("File not found");
            }

            using (var fileStream = File.OpenRead(apPath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        var from = values[0];
                        var to = values[1];
                        var distance = double.Parse(values[2]);
                        if (!nodeDict.ContainsKey(from)) { nodeDict.Add(from, new Node(from)); }
                        if (!nodeDict.ContainsKey(to)) { nodeDict.Add(to, new Node(to)); }
                        unvisited.Add(from);
                        unvisited.Add(to);
                        routes.Add(new Route(from, to, distance));
                    }
                }
            }
        }

        private void CheckNode(PrioQueue queue, string destinationNode)
        {
            if (queue.Count == 0)
            {
                return;
            }

            foreach (var route in routes.FindAll(r => r.From == queue.First.Value.Name))
            {
                if (!unvisited.Contains(route.To))
                {
                    continue;
                }

                double travelledDistance = nodeDict[queue.First.Value.Name].Value + route.Cost;

                if (travelledDistance < nodeDict[route.To].Value)
                {
                    nodeDict[route.To].Value = travelledDistance;
                    nodeDict[route.To].PreviousNode = nodeDict[queue.First.Value.Name];
                }

                if (!queue.HasLetter(route.To))
                {
                    queue.AddNodeWithPriority(nodeDict[route.To]);
                }
            }

            unvisited.Remove(queue.First.Value.Name);
            queue.RemoveFirst();
            CheckNode(queue, destinationNode);
        }

        private void PrintShortestPath(string startNode, string destNode)
        {
            var pathList = new List<String> { destNode };

            Node currentNode = nodeDict[destNode];
            while (currentNode != nodeDict[startNode])
            {
                pathList.Add(currentNode.PreviousNode.Name);
                currentNode = currentNode.PreviousNode;
            }
            pathList.Reverse();
            var sentence = string.Empty;
            for (int i = 0; i < pathList.Count; i++)
            {
                sentence += pathList[i] + (i < pathList.Count - 1 ? "-" : "");
            }
            Result = new Route(startNode, destNode, nodeDict[destNode].Value) { RoutePath = sentence };
            //_result.Add(new Route("","", nodeDict[destNode].Value) { RoutePath = sentence });
            //_result.Add(nodeDict[destNode].Value.ToString());
        }
    }
}