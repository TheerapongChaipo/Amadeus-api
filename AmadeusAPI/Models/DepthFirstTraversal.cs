using System.Collections.Generic;

namespace AmadeusAPI.Models
{
    public class DepthFirstTraversal
    {
        private readonly List<string> _result = new List<string>();
        public IList<string> Result { get { return _result; } }

        private int vertices;
        private List<int>[] adjList;

        public Dictionary<string, int> datacode;
        public DepthFirstTraversal(int vertices)
        {
            this.vertices = vertices;
            initAdjList();

            datacode = new Dictionary<string, int>()
            {
                {"A", 0},
                {"B", 1},
                {"C", 2},
                {"D", 3},
                {"E", 4},
                {"F", 5},
                {"G", 6},
                {"H", 7},
                {"I", 8},
            };
        }

        private void initAdjList()
        {
            adjList = new List<int>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                adjList[i] = new List<int>();
            }
        }

        public void addEdge(int u, int v)
        {
            adjList[u].Add(v);
        }

        public void printAllPaths(int source, int destination)
        {
            bool[] isVisited = new bool[vertices];
            List<int> pathList = new List<int>();
            pathList.Add(source);
            GetAllPathsUtil(source, destination, isVisited, pathList);
        }
        private void GetAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList)
        {
            isVisited[u] = true;
            if (u.Equals(d))
            {
                var sybol = string.Empty;
                var sentence = string.Empty;              
                for (int i = 0; i < localPathList.Count; i++)
                {
                    sentence += Data.GetCode(localPathList[i]) + (i < localPathList.Count - 1 ? "-" : "");
                }
                _result.Add(sentence);
                isVisited[u] = false;
                return;
            }

            foreach (int i in adjList[u])
            {
                if (!isVisited[i])
                {
                    localPathList.Add(i);
                    GetAllPathsUtil(i, d, isVisited, localPathList);
                    localPathList.Remove(i);
                }
            }
            isVisited[u] = false;
        }

       
    }
}