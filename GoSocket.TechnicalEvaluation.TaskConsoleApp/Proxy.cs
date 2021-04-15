using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoSocket.TechnicalEvaluation.TaskConsoleApp
{
    public class Proxy
    {
        readonly string BaseAddress = ConfigurationManager.AppSettings["UrlWebApi"];        

        

        public async Task<string> SendGetAsync(string requestURI, string fullRequestURI = null)
        {
            string ResultJSON = string.Empty;
            using (var Client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(requestURI))
                    {
                        requestURI = fullRequestURI;
                    }
                    else
                    {
                        requestURI = BaseAddress + requestURI; // URL Absoluto
                    }

                    Client.DefaultRequestHeaders.Accept.Clear();
                    
                    Client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));

                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    ResultJSON = await Client.GetStringAsync(requestURI);
                }
                catch (Exception)
                {
                    // Manejar la excepción
                }
            }
            return ResultJSON;
        }

        public string SendGet(string requestURI, string fullRequestURI = null)
        {
            string Result = default(string);
            Task.Run(async () => Result = await SendGetAsync(requestURI, fullRequestURI)).Wait();
            return Result;
        }
    }
}
