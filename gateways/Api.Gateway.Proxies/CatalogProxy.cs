using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{
    public interface ICatalogProxy
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        //Task<ProductDto> GetAsync(int id);
    }
    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;
        public CatalogProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }


        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}v1/product");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
