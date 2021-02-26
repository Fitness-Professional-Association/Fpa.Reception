﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class HttpClientExtensions
    {
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static async Task<HttpResponseMessage> GetAsync(this System.Net.Http.HttpClient client, string url, object content)
        {
            var json = JsonConvert.SerializeObject(content, serializerSettings);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.SendAsync(request).ConfigureAwait(false);

            return result;
        }

        public static async Task<T> GetResultAsync<T>(this HttpResponseMessage request)
        {
            var content = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }
    }
}
