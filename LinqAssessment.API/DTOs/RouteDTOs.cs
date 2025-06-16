namespace LinqAssessment.API.DTOs;

public class FlightDto
{
    public int FlightId { get; set; }
    public string Provider { get; set; } = null!;
    public decimal Price { get; set; }
    public string Departure { get; set; } = null!;
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public int Duration { get; set; }
}

public class JourneyDto
{
    public List<FlightDto> Flights { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public int TotalDuration { get; set; }
    public int ExchangeCount { get; set; }
}

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
} 