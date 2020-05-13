using System;
using System.IO;
using System.Threading.Tasks;
using ABD_cw3.Services;
using Microsoft.AspNetCore.Http;

namespace ABD_cw3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string filePath = "logi.txt";
            string fullRequest = httpContext.Request.Method.ToString() + " " +
                                 httpContext.Request.Path;
            if (httpContext.Request.ContentLength == null)
            {
                fullRequest += httpContext.Request.Body.ToString();
            }
            else fullRequest += "<Empty body> ";

            if (httpContext.Request.QueryString.ToString().Length == 0)
            {
                fullRequest += "<Empty Query Params> ";
            }
            else fullRequest += httpContext.Request?.QueryString.ToString();

            using (StreamWriter sw = !File.Exists(filePath) ? File.CreateText(filePath) : File.AppendText(filePath))
            {
                sw.WriteLine(fullRequest);
                sw.WriteLine();
            }
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next(httpContext);
        }
    }
}
