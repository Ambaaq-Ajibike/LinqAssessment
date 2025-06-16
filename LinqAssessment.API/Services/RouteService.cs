using LinqAssessment.API.Data;
using LinqAssessment.API.DTOs;
using LinqAssessment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqAssessment.API.Services;

public class RouteService : IRouteService
{
    private readonly ApplicationDbContext _context;

    public RouteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JourneyDto>> GetRoutesWithMinExchangesAsync(string origin, string destination, bool ascending = true)
    {
        var routes = await FindAllPossibleRoutesAsync(origin, destination);
        var journeys = routes.Select(r => new JourneyDto
        {
            Flights = r.Select(f => new FlightDto
            {
                FlightId = f.FlightId,
                Provider = f.Provider,
                Price = f.Price,
                Departure = f.Departure,
                From = f.Route.From,
                To = f.Route.To,
                Duration = f.Route.Duration
            }).ToList(),
            TotalPrice = r.Sum(f => f.Price),
            TotalDuration = r.Sum(f => f.Route.Duration),
            ExchangeCount = r.Count - 1
        }).ToList();

        return ascending
            ? journeys.OrderBy(j => j.ExchangeCount).ToList()
            : journeys.OrderByDescending(j => j.ExchangeCount).ToList();
    }

    public async Task<List<JourneyDto>> GetRoutesWithMinPriceAsync(string origin, string destination, string departure, bool ascending = true)
    {
        var routes = await FindAllPossibleRoutesAsync(origin, destination, departure);
        var journeys = routes.Select(r => new JourneyDto
        {
            Flights = r.Select(f => new FlightDto
            {
                FlightId = f.FlightId,
                Provider = f.Provider,
                Price = f.Price,
                Departure = f.Departure,
                From = f.Route.From,
                To = f.Route.To,
                Duration = f.Route.Duration
            }).ToList(),
            TotalPrice = r.Sum(f => f.Price),
            TotalDuration = r.Sum(f => f.Route.Duration),
            ExchangeCount = r.Count - 1
        }).ToList();

        return ascending
            ? journeys.OrderBy(j => j.TotalPrice).ToList()
            : journeys.OrderByDescending(j => j.TotalPrice).ToList();
    }

    public async Task<List<JourneyDto>> GetRoutesWithMinDurationAsync(string origin, string destination, string departure, bool ascending = true)
    {
        var routes = await FindAllPossibleRoutesAsync(origin, destination, departure);
        var journeys = routes.Select(r => new JourneyDto
        {
            Flights = r.Select(f => new FlightDto
            {
                FlightId = f.FlightId,
                Provider = f.Provider,
                Price = f.Price,
                Departure = f.Departure,
                From = f.Route.From,
                To = f.Route.To,
                Duration = f.Route.Duration
            }).ToList(),
            TotalPrice = r.Sum(f => f.Price),
            TotalDuration = r.Sum(f => f.Route.Duration),
            ExchangeCount = r.Count - 1
        }).ToList();

        return ascending
            ? journeys.OrderBy(j => j.TotalDuration).ToList()
            : journeys.OrderByDescending(j => j.TotalDuration).ToList();
    }

    public async Task<PaginatedResponse<JourneyDto>> GetAllJourneysAsync(int page = 1, int size = 100, bool ascending = true)
    {
        var allRoutes = await _context.Routes
            .Include(r => r.Flights)
            .ToListAsync();

        var allJourneys = new List<JourneyDto>();
        foreach (var route in allRoutes)
        {
            foreach (var flight in route.Flights)
            {
                allJourneys.Add(new JourneyDto
                {
                    Flights = new List<FlightDto>
                    {
                        new()
                        {
                            FlightId = flight.FlightId,
                            Provider = flight.Provider,
                            Price = flight.Price,
                            Departure = flight.Departure,
                            From = route.From,
                            To = route.To,
                            Duration = route.Duration
                        }
                    },
                    TotalPrice = flight.Price,
                    TotalDuration = route.Duration,
                    ExchangeCount = 0
                });
            }
        }

        var orderedJourneys = ascending
            ? allJourneys.OrderBy(j => j.TotalPrice).ToList()
            : allJourneys.OrderByDescending(j => j.TotalPrice).ToList();

        var totalCount = orderedJourneys.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)size);
        var items = orderedJourneys
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return new PaginatedResponse<JourneyDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = size,
            TotalPages = totalPages
        };
    }

    private async Task<List<List<Flight>>> FindAllPossibleRoutesAsync(string origin, string destination, string? departure = null)
    {
        var routes = new List<List<Flight>>();
        var visited = new HashSet<string>();

        async Task FindRoutes(string current, List<Flight> currentPath)
        {
            if (current == destination)
            {
                routes.Add(new List<Flight>(currentPath));
                return;
            }

            var flights = await _context.Flights
                .Include(f => f.Route)
                .Where(f => f.Route.From == current)
                .ToListAsync();

            foreach (var flight in flights)
            {
                if (departure != null && flight.Departure.CompareTo(departure) < 0)
                    continue;

                if (visited.Contains(flight.RouteId))
                    continue;

                visited.Add(flight.RouteId);
                currentPath.Add(flight);
                await FindRoutes(flight.Route.To, currentPath);
                currentPath.RemoveAt(currentPath.Count - 1);
                visited.Remove(flight.RouteId);
            }
        }

        await FindRoutes(origin, new List<Flight>());
        return routes;
    }
} 