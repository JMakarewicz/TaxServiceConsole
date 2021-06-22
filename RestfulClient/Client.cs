using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestfulClient
{
    //As per spec:  We are going to use Tax Jar as one of our calculators.
    //You will need to write a .Net client to talk to their API, do not use theirs.  
    public class Client
    {
        //See https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/
        //for details.
        protected HttpClient client = null;

        public Client(string token)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public string SendGetRequest(string url, List<KeyValuePair<string, string>> parameters)
        {
            string result = String.Empty, queryString = String.Empty;
            HttpRequestMessage message = new HttpRequestMessage();
            Uri destination = null;
            Task<string> worker = null;

            message.Method = HttpMethod.Get;
            message.RequestUri = destination;
            queryString = ToQueryString(parameters);
            destination = new Uri(String.Concat(url, queryString));
            message.RequestUri = destination;
            worker = Task.Run(()=>SendMessage(message));
            worker.Wait();
            result = worker.Result;

            return result;
        }

        public string SendPostRequest(string url, List<KeyValuePair<string, string>> parameters)
        {
            string payload = ToJsonString(parameters);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            Uri destination = new Uri(url);
            string result = null;
            Task<string> worker = Task.Run(() => PostContent(destination, content));
            
            worker.Wait();
            result = worker.Result;

            return result;
        }

        protected async Task<string> SendMessage(HttpRequestMessage message)
        {
            string result = String.Empty;
            HttpResponseMessage response = null;

            response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = String.Concat("Result:  ", response.StatusCode.ToString());
            }

            return result;
        }

        protected async Task<string> PostContent(Uri destination, HttpContent content)
        {
            string result = String.Empty;
            HttpResponseMessage response = null;

            response = await client.PostAsync(destination, content);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = String.Concat("Result:  ", response.StatusCode.ToString());
            }

            return result;
        }

        protected string ToQueryString(List<KeyValuePair<string, string>> parameters)
        {
            string[] array =
                (
                    from pair in parameters
                    select String.Format("{0}={1}", HttpUtility.UrlEncode(pair.Key),
                    HttpUtility.UrlEncode(pair.Value))
                ).ToArray();

            return String.Concat("?", String.Join("&", array));
        }
        
        protected string ToJsonString(List<KeyValuePair<string, string>> parameters)
        {
            string[] array =
                (
                    from pair in parameters
                    select String.Format("\"{0}\":\"{1}\"", HttpUtility.UrlEncode(pair.Key), 
                    HttpUtility.UrlEncode(pair.Value))
                ).ToArray();

            return String.Concat("{", String.Join(",", array), "}");
        }
    }
}
