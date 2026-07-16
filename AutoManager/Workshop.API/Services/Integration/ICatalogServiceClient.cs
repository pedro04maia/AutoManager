using Workshop.API.DTOs;

namespace Workshop.API.Services.Integration
{
    public interface ICatalogServiceClient
    {
        Task<PartAvailabilityResponseDto?> CheckPartAvailabilityAsync(int partId, int quantity);
    }
}
