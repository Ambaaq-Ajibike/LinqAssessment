using System.ComponentModel.DataAnnotations;

namespace LinqAssessment.API.Models;

public class Route
{
    [Key]
    public string RouteId { get; set; } = null!;
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public int Duration { get; set; }
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
} 