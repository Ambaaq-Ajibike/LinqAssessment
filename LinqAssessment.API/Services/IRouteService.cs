using LinqAssessment.API.DTOs;

namespace LinqAssessment.API.Services;

public interface IRouteService
{
    Task<List<JourneyDto>> GetRoutesWithMinExchangesAsync(string origin, string destination, bool ascending = true);
    Task<List<JourneyDto>> GetRoutesWithMinPriceAsync(string origin, string destination, string departure, bool ascending = true);
    Task<List<JourneyDto>> GetRoutesWithMinDurationAsync(string origin, string destination, string departure, bool ascending = true);
    Task<PaginatedResponse<JourneyDto>> GetAllJourneysAsync(int page = 1, int size = 100, bool ascending = true);
} 