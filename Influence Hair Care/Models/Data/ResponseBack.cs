using System.Net;

namespace Influence_Hair_Care.Models.Data
{
    public class ResponseBack<T> where T : class
    {
        //public string Status { get; set; }
        //public string Message { get; set; }
        //public T Data { get; set; }

        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = "Success";
    }
}
