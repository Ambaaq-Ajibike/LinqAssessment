using LinqAssessment.API.Models;

namespace LinqAssessment.API.Data;

public static class SampleDataSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        if (context.Routes.Any() || context.Flights.Any())
            return;

        // Add sample routes
        var routes = new List<LinqAssessment.API.Models.Route>
        {
            new() { RouteId = "A_B", From = "A", To = "B", Duration = 5 },
            new() { RouteId = "B_C", From = "B", To = "C", Duration = 8 },
            new() { RouteId = "A_C", From = "A", To = "C", Duration = 12 },
            new() { RouteId = "C_D", From = "C", To = "D", Duration = 6 },
            new() { RouteId = "B_D", From = "B", To = "D", Duration = 9 }
        };

        context.Routes.AddRange(routes);
        context.SaveChanges();

        // Add sample flights
        var flights = new List<Flight>
        {
            new() { RouteId = "A_B", Provider = "Virgin", Price = 210, Departure = "+1 day 5 hour" },
            new() { RouteId = "A_B", Provider = "British Airways", Price = 180, Departure = "+1 day 8 hour" },
            new() { RouteId = "B_C", Provider = "Virgin", Price = 150, Departure = "+2 day 2 hour" },
            new() { RouteId = "B_C", Provider = "British Airways", Price = 170, Departure = "+2 day 4 hour" },
            new() { RouteId = "A_C", Provider = "Virgin", Price = 300, Departure = "+1 day 10 hour" },
            new() { RouteId = "C_D", Provider = "British Airways", Price = 120, Departure = "+3 day 1 hour" },
            new() { RouteId = "B_D", Provider = "Virgin", Price = 200, Departure = "+2 day 6 hour" }
        };

        context.Flights.AddRange(flights);
        context.SaveChanges();
    }
} 