using CAPA.HTTP.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CAPA.HTTP.Servicio
{
    public class PeticionesHTTP : IPeticionesHTTP
    {
        private readonly HttpClient _httpClient;
        protected string _UrlBase;
        
        public PeticionesHTTP()
        {
            _httpClient = new HttpClient();
        }

        public void SetUrlBase(string url)
        {
            _UrlBase = url;
        }

        #region POST
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string metodo, TRequest payload, string Token)
        {
            if (string.IsNullOrEmpty(_UrlBase))
                throw new AccessViolationException("NO TIENE LA URL BASE");

            _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string url = $"{_UrlBase}{metodo}";

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseJson);
        }

        public TResponse Post<TRequest, TResponse>(string metodo, TRequest payload, string Token)
        {
            if (string.IsNullOrEmpty(_UrlBase))
                throw new AccessViolationException("NO TIENE LA URL BASE");

            _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string url = $"{_UrlBase}{metodo}";

            var response = _httpClient.PostAsync(url, content).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<TResponse>(responseJson);
        }
        #endregion

        #region GET
        public async Task<TResponse> GetAsync<TResponse>(string metodo, string Token, object queryParams = null)
        {
            string url = $"{_UrlBase}{metodo}";

            if (queryParams != null)
            {
                var query = ToQueryString(queryParams);
                url = $"{url}?{query}";
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var resul = JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resul;
        }

        public TResponse Get<TResponse>(string metodo, string Token, object queryParams = null)
        {
            string url = $"{_UrlBase}{metodo}";

            if (queryParams != null)
            {
                var query = ToQueryString(queryParams);
                url = $"{url}?{query}";
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var resul = JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resul;
        }
        #endregion

        #region DELETE
        public async Task<TResponse> DeleteAsync<TResponse>(string metodo, string Token, object queryParams = null)
        {
            string url = $"{_UrlBase}{metodo}";

            if (queryParams != null)
            {
                var query = ToQueryString(queryParams);
                url = $"{url}?{query}";
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var resul = JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resul;
        }

        public TResponse Delete<TResponse>(string metodo, string Token, object queryParams = null)
        {
            string url = $"{_UrlBase}{metodo}";

            if (queryParams != null)
            {
                var query = ToQueryString(queryParams);
                url = $"{url}?{query}";
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var resul = JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resul;
        }
        #endregion

        private string ToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj, null).ToString())}";

            return string.Join("&", properties);
        }
    }
}
