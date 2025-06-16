using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqAssessment.API.Models;

public class Flight
{
    [Key]
    public int FlightId { get; set; }
    public string RouteId { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public decimal Price { get; set; }
    public string Departure { get; set; } = null!;
    
    [ForeignKey("RouteId")]
    public Route Route { get; set; } = null!;
} 