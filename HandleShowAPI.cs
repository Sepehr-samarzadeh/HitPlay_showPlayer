using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HitPlay
{
    public class HandleShowAPI
    {
        private readonly HttpClient _httpClient;
        public HandleShowAPI()
        {
            _httpClient = new HttpClient();
        }
        public async Task<TrackResponse> GetApiDataAsync(string url, string? bearerToken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);


            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }


            var response = await _httpClient.SendAsync(request);


            response.EnsureSuccessStatusCode();


            var responseContent = await response.Content.ReadAsStringAsync();



            
            return JsonConvert.DeserializeObject<TrackResponse>(responseContent);

        }
    }
}
