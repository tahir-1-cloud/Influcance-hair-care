using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System;

namespace Influence_Hair_Care.Models.Data
{
    public class RequestSender
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void SetIHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        private static RequestSender instance;
        public static RequestSender Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestSender();
                }
                return instance;
            }
        }
        private RequestSender()
        {

        }
        public class SResponse
        {
            public SResponse()
            {

            }
            public string Error { get; set; }
            public int ErrorCode { get; set; }
            public string key { get; set; }
            public string msg { get; set; }
            public string Resp { get; set; }
            public bool Status { get; set; }
        }
        public enum Requestmethod
        {
            GET,
            POST,
            DELETE,
            PUT,
        }
        //https://localhost:44301/
        public SResponse CallAPI(string otype, string action, string method, string inpobject = "", string token = "")
        {
            //string AccessToken = string.Empty;
            SResponse waresp = new SResponse();
            string resp = "";
            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.18.37:8060/" + otype + "/" + action);

               // HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://45.35.85.135:8050/" + otype + "/" + action);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44333/" + otype + "/" + action);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = method;
                if (System.Diagnostics.Debugger.IsAttached)
                    request.Timeout = 120000;
                else
                    request.Timeout = 40000;
                request.ContentType = "application/json";
                request.ContentLength = 0;
                if (inpobject != "")
                {
                    byte[] byteData = Encoding.UTF8.GetBytes(inpobject);
                    request.ContentLength = byteData.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(byteData, 0, byteData.Length);
                    stream.Close();
                }
                ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(
                delegate (
                object sender2,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                });
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                resp = sr.ReadToEnd();
                waresp.Status = true;
                waresp.Resp = resp;
                return waresp;
            }
            catch (WebException wex)
            {
                waresp.Status = false;
                waresp.Error = wex.Message;
                if (wex.Message == "error: (403) Forbidden.")
                    waresp.ErrorCode = 403;
                else if (wex.Message == "The remote server returned an error: (401) Unauthorized.")
                    waresp.ErrorCode = 401;
                else
                    waresp.ErrorCode = 0;
                waresp.Resp = wex.Message;
                return waresp;
            }
            catch (Exception ex)
            {
                waresp.Status = false;
                waresp.Error = ex.Message;
                waresp.ErrorCode = 0;
                return waresp;
            }
            finally { }

        }
    }
}
