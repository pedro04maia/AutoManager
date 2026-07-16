using System.Net.Http.Json;
using Workshop.API.DTOs;

namespace Workshop.API.Services.Integration
{
    public class CatalogServiceClient
    {
        private readonly HttpClient _httpClient;

        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PartAvailabilityResponseDto?> CheckPartAvailabilityAsync(int partId, int quantity)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/parts/{partId}/availability?quantity={quantity}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<PartAvailabilityResponseDto>();
            }
            catch (HttpRequestException)
            {
                return null; // Se o catálogo estiver offline, devolve null (Resiliência)
            }
        }
    }
}
