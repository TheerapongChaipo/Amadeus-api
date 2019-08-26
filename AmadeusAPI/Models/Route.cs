using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmadeusAPI.Models
{
    public class Route
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public double Distance { get; private set; }

        public Route(string from, string to, double distance)
        {
            this.From = from;
            this.To = to;
            this.Distance = distance;
        }
    }
}