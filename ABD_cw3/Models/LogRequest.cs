using System;
namespace ABD_cw3.Models
{
    public class LogRequest
    {
        public string Method { get; set; }
        public string PathRequest { get; set; }
        public string BodyRequest { get; set; }
        public string QueryParams { get; set; }
    }
}
