using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Universal
    {
        public async Task<T> GET<T>(string relativeUri, string id, bool apiKeyRequired)
        {
            string json = "";
            string asyncUri = apiKeyRequired ? $"{relativeUri}{id}{apiKey}" : $"{relativeUri}{id}";

            HttpResponseMessage responseMsg = await httpClient.GetAsync(asyncUri);
            json = await responseMsg.Content.ReadAsStringAsync();

            if (responseMsg.IsSuccessStatusCode)
            {
                Console.WriteLine($"[@{relativeUri}]Status: {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}");
            }
            else if (responseMsg.StatusCode == HttpStatusCode.Forbidden)
            {
                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Seems like you are banned... {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
                else
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
            }
            else
            {
                Console.WriteLine(json);
                Console.WriteLine();
                Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                inErrorState = true;
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> GET<T>(string relativeUri, bool apiKeyRequired)
        {
            string json = "";
            string asyncUri = apiKeyRequired ? $"{relativeUri}{apiKey}" : relativeUri;

            HttpResponseMessage responseMsg = await httpClient.GetAsync(asyncUri);
            json = await responseMsg.Content.ReadAsStringAsync();

            if (responseMsg.IsSuccessStatusCode)
            {
                Console.WriteLine($"[@{relativeUri}]Status: {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}");
            }
            else if (responseMsg.StatusCode == HttpStatusCode.Forbidden)
            {
                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Seems like you are banned... {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
                else
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
            }
            else
            {
                Console.WriteLine(json);
                Console.WriteLine();
                Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                inErrorState = true;
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> GET<T>(string relativeUri, bool apiKeyRequired, string parameters)
        {
            string json = "";
            string asyncUri = apiKeyRequired ? $"{relativeUri}{apiKey}{parameters}" : $"{relativeUri}{parameters}";

            HttpResponseMessage responseMsg = await httpClient.GetAsync(asyncUri);
            json = await responseMsg.Content.ReadAsStringAsync();

            if (responseMsg.IsSuccessStatusCode)
            {
                Console.WriteLine($"[@{relativeUri}]Status: {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}");
            }
            else if (responseMsg.StatusCode == HttpStatusCode.Forbidden)
            {
                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Seems like you are banned... {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
                else
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
            }
            else
            {
                Console.WriteLine(json);
                Console.WriteLine();
                Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                inErrorState = true;
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> GET<T>(string relativeUri, string id, string parameters, bool apiKeyRequired)
        {
            string json = "";
            string asyncUri = apiKeyRequired ? $"{relativeUri}{id}{parameters}{apiKey}" : $"{relativeUri}{id}{parameters}";

            HttpResponseMessage responseMsg = await httpClient.GetAsync(asyncUri);
            json = await responseMsg.Content.ReadAsStringAsync();

            if (responseMsg.IsSuccessStatusCode)
            {
                Console.WriteLine($"[@{relativeUri}]Status: {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}");
            }
            else if (responseMsg.StatusCode == HttpStatusCode.Forbidden)
            {
                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Seems like you are banned... {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
                else
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
            }
            else
            {
                Console.WriteLine(json);
                Console.WriteLine();
                Console.WriteLine($"Unexpected Error! --- {responseMsg.StatusCode} --- {responseMsg.ReasonPhrase}\n{json}");
                inErrorState = true;
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> POST<T>(string url, StringContent content, bool apiKeyRequired)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string relativeUrl = apiKeyRequired ? $"{url}{apiKey}" : url;

            HttpResponseMessage res = await httpClient.PostAsync(relativeUrl, content);
            string json = await res.Content.ReadAsStringAsync();

            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine($"[@{relativeUrl}]Status: {res.StatusCode} --- {res.ReasonPhrase}");
            }
            else if (res.StatusCode == HttpStatusCode.Forbidden)
            {
                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Seems like you are banned... {res.StatusCode} --- {res.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
                else
                {
                    Console.WriteLine(json);
                    Console.WriteLine();
                    Console.WriteLine($"Unexpected Error! --- {res.StatusCode} --- {res.ReasonPhrase}\n{json}");
                    inErrorState = true;
                }
            }
            else
            {
                Console.WriteLine(json);
                Console.WriteLine();
                Console.WriteLine($"Unexpected Error! --- {res.StatusCode} --- {res.ReasonPhrase}\n{json}");
                inErrorState = true;
            }

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}