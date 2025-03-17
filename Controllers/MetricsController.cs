using Microsoft.AspNetCore.Mvc;
using Worthy_API.Helpers;
using Worthy_API.Interfaces;

namespace Worthy_API.Controllers;

[Route("api/metrics")]
[ApiController]
public class MetricsController : Controller
{
    private readonly IMetricsRepository _metricsRepository;

    public MetricsController(IMetricsRepository metricsRep)
    {
        _metricsRepository = metricsRep;
    }

    // This action is for getting all metrics with the query parameter
    [HttpGet]  // This distinguishes the action with the /api/metrics/all route
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var metrics = await _metricsRepository.GetAllAsync(query);
        return Ok(metrics);
    }

    // This action is for returning a view (MVC)
    [HttpGet("index")]  // This distinguishes the action with the /api/metrics/index route
    public IActionResult Index()
    {
        return View();
    }
}