using AmadeusAPI.Models;
using System.Collections.Generic;

namespace AmadeusAPI.Controllers
{
    public class SearchReq
    {
        public string source { get; set; }
        public string destination { get; set; }
    }

    public class SearchResponse
    {
        public int Messagecode { get; set; }
        public string MessageDes { get; set; }
    }

    public class ShortestResponse
    {      
        public string Routepath { get; set; }
        public double Cost  { get; set; }
        public List<Stations> Stations { get; set; }
    }
}