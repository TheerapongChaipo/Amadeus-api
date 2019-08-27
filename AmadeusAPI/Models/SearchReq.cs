namespace AmadeusAPI.Controllers
{
    public class SearchReq
    {
        public string source { get; set; }
        public string destination { get; set; }
    }

    public class SearchResponse
    {
        public int messagecode { get; set; }
        public string messagedes { get; set; }
    }
}