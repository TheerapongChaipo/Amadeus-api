using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmadeusAPI.Models
{
    public class DepthFirstTraversal
    {
        private readonly List<string> _result = new List<string>();
        public IList<string> Result { get { return _result; } }

        private int vertices;                                       // No. of vertices in graph        
        private List<int>[] adjList;                                // adjacency list          
        public DepthFirstTraversal(int vertices)                                  // Constructor  
        {           
            this.vertices = vertices;                               // initialise vertex count  
            initAdjList();                                          // initialise adjacency list 
        }

        // utility method to initialise  
        // adjacency list  
        private void initAdjList()
        {
            adjList = new List<int>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                adjList[i] = new List<int>();
            }
        }

        // add edge from u to v  
        public void addEdge(int u, int v)
        {
            // Add v to u's list.  
            adjList[u].Add(v);
        }

       
       
        public void printAllPaths(int source, int destination)              // Prints all paths from   // 's' to 'd'  
        {
            bool[] isVisited = new bool[vertices];
            List<int> pathList = new List<int>();            
            pathList.Add(source);                                           // add source to path[]
            GetAllPathsUtil(source, destination, isVisited, pathList);    // Call recursive utility  
        }

        // A recursive function to print  
        // all paths from 'u' to 'd'.  
        // isVisited[] keeps track of  
        // vertices in current path.  
        // localPathList<> stores actual  
        // vertices in the current path  
        private void GetAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList)
        {
            List<string> result = new List<string>();
            // Mark the current node  
            isVisited[u] = true;

            if (u.Equals(d))
            {
                var sybol = string.Empty;
                var sentence = string.Empty;
                foreach (var item in localPathList)
                {
                    switch (item)
                    {
                        case 0:
                            sybol = "A";
                            break;
                        case 1:
                            sybol = "B";
                            break;
                        case 2:
                            sybol = "C";
                            break;
                        case 3:
                            sybol = "D";
                            break;
                        case 4:
                            sybol = "E";
                            break;
                        case 5:
                            sybol = "F";
                            break;
                        case 6:
                            sybol = "G";
                            break;
                        case 7:
                            sybol = "H";
                            break;
                        case 8:
                            sybol = "I";
                            break;
                        default:
                            break;
                    }
                    sentence += sybol + "-";
                }
                _result.Add(sentence);
                //Console.WriteLine(string.Join("-", localPathList));
                //Console.WriteLine(sentence);
                
                // if match found then no need  
                // to traverse more till depth  
                isVisited[u] = false;
                return;
            }

            // Recur for all the vertices  
            // adjacent to current vertex  
            foreach (int i in adjList[u])
            {
                if (!isVisited[i])
                {
                    // store current node  
                    // in path[]  
                    localPathList.Add(i);
                    GetAllPathsUtil(i, d, isVisited, localPathList);

                    // remove current node  
                    // in path[]  
                    localPathList.Remove(i);
                }
            }
            
            isVisited[u] = false;  // Mark the current node  
        }
     
    }
}