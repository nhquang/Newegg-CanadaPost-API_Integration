using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace API
{
    public static class HttpRequest
    {
        public static async Task<HttpWebRequest> createRequest(string URI, string method, Dictionary<string, string> parameters, Dictionary<string, string> headers = null, string contentType = null, string accept = null, string body = null)
        {

            try
            {
                //URI Params
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (i == 0) URI += "?";
                        else URI += "&";
                        URI += parameters.ElementAt(i).Key + "=" + parameters.ElementAt(i).Value;
                    }
                }
                //create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
                request.Method = method;
                
                //request headers
                if(headers != null)
                    foreach(var item in headers)
                        request.Headers.Add(item.Key, item.Value);

                request.ContentType = string.IsNullOrEmpty(contentType) ? "application/json" : contentType;
                request.Accept = string.IsNullOrEmpty(accept) ? "application/json" : accept;
                //request body
                if (!string.IsNullOrEmpty(body))
                {
                    /*using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        await streamWriter.WriteAsync(body);
                        streamWriter.Close();
                    }*/
                    using (var stream = await request.GetRequestStreamAsync())
                    {
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] buffer = encoding.GetBytes(body);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                    }
                }
                
                return request;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static async Task<string> getData(HttpWebRequest request)
        {
            var jsonResponse = string.Empty;

            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(stream))
                        {
                            jsonResponse = await sr.ReadToEndAsync();
                            sr.Close();
                        }
                        stream.Close();
                    }
                    response.Close();
                }
                return jsonResponse;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        class ErrMessage
        {
            public int code { get; set; }
        }
    }
}