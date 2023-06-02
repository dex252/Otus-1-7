using RestSharp;
using System;
using System.Threading.Tasks;

namespace WebClient.Http
{
    internal class HttpClient : IHttpClient
    {
        private RestClient Client { get; }
        public HttpClient(string url)
        {
            Client = new RestClient(url);
        }

        public async Task<Customer> GetCustomerAsync<T>(T id)
        {
            var request = new RestRequest($"customers/{id}", Method.Get);
            var response = await Client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(response.Content);
            }

            throw new Exception($"{response.StatusCode}: {response.ErrorMessage}");
        }

        public async Task<long> CreateCustomerAsync<T>(T content)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);

            var request = new RestRequest("customers", Method.Post);
            request.AddBody(json);
            
            var response = await Client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<long>(response.Content);
            }

            throw new Exception($"{response.StatusCode}: {response.Content}");
        }
    }
}
