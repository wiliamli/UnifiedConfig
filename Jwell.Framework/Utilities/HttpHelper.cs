using System.IO;
using System.Net;
using System.Text;

namespace Jwell.Framework.Utilities
{
    public class HttpHelper
    {
        public static string HttpPost(string confUrl, string body,string header, int timeout = 2000)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body ?? string.Empty);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(confUrl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json;charset=UTF-8";
            httpWebRequest.Timeout = timeout;
            httpWebRequest.ContentLength = bytes.Length;
            httpWebRequest.Headers.Add("USERDEFINED", header);
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static string HttpGet(string confUrl, int timeout = 1000)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(confUrl);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "text/html";
            httpWebRequest.Timeout = timeout;
            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
