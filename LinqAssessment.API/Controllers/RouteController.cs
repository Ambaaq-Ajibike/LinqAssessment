using LinqAssessment.API.DTOs;
using LinqAssessment.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinqAssessment.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RouteController(IRouteService routeService) : ControllerBase
{
    private readonly IRouteService _routeService = routeService;

    [HttpGet("minimize-exchanges")]
    public async Task<ActionResult<List<JourneyDto>>> GetRoutesWithMinExchanges(
        [FromQuery] string origin,
        [FromQuery] string destination,
        [FromQuery] bool ascending = true)
    {
        try
        {
            var routes = await _routeService.GetRoutesWithMinExchangesAsync(origin, destination, ascending);
            return Ok(routes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("minimize-price")]
    public async Task<ActionResult<List<JourneyDto>>> GetRoutesWithMinPrice(
        [FromQuery] string origin,
        [FromQuery] string destination,
        [FromQuery] string departure,
        [FromQuery] bool ascending = true)
    {
        try
        {
            var routes = await _routeService.GetRoutesWithMinPriceAsync(origin, destination, departure, ascending);
            return Ok(routes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("minimize-duration")]
    public async Task<ActionResult<List<JourneyDto>>> GetRoutesWithMinDuration(
        [FromQuery] string origin,
        [FromQuery] string destination,
        [FromQuery] string departure,
        [FromQuery] bool ascending = true)
    {
        try
        {
            var routes = await _routeService.GetRoutesWithMinDurationAsync(origin, destination, departure, ascending);
            return Ok(routes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<PaginatedResponse<JourneyDto>>> GetAllJourneys(
        [FromQuery] int page = 1,
        [FromQuery] int size = 100,
        [FromQuery] bool ascending = true)
    {
        try
        {
            var journeys = await _routeService.GetAllJourneysAsync(page, size, ascending);
            return Ok(journeys);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 