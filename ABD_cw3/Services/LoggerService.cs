using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ABD_cw3.Services
{
    public class LoggerService
    {
        public LoggerService()
        {
        }

        public static void saveLogs(HttpContext httpContext)
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
        }
    }
}
