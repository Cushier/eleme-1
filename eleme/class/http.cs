using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace http
{
    class e_http
    {

        public string HttpPost(string Url, string postDataStr, string cookie)
        {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "Content-Type: application/json; charset=UTF-8";
                request.ContentLength = postDataStr.Length;
                request.Headers.Add("cookie", cookie);
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Flush();
              HttpWebResponse res = null;

             try
            {
                res = (HttpWebResponse)request.GetResponse();


            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;

            }
            StreamReader text = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            return text.ReadToEnd() + "";
        }
        public string HttpGet(string Url, string cookie) {
            HttpWebResponse res = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(Url));
                request.Headers.Add("cookie",cookie);
                res = (HttpWebResponse)request.GetResponse();


            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;

            }
            StreamReader text = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            return(text.ReadToEnd() + "");
        }

    }
}