using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmadeusAPI.Models
{
    public class Node
    {
        public string Name { get; private set; }
        public double Value { get; set; }
        public Node PreviousNode { get; set; }

        public Node(string name, double value = int.MaxValue, Node previousNode = null)
        {
            this.Name = name;
            this.Value = value;
            this.PreviousNode = previousNode;
        }

    }

    public class Stations {      
        public int Sequence { get; set; }
        public string Name { get; set; } 
        public string Routepath { get; set; }   
    }


    public class Data
    {
        public static string GetCode(int code)
        {
            switch (code)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "G";
                case 7:
                    return "H";
                case 8:
                    return "I";
                default:
                    return "";
            }
        }
    }
}