using Microsoft.AspNetCore.Mvc;
using Worthy_API.Dtos.Metrics;
using Worthy_API.Helpers;
using Worthy_API.Interfaces;
using Worthy_API.Mappers;
using Worthy_API.Models;

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
        var metricsDto = metrics.Select(m => m.ToMetircsDto());
        return Ok(metricsDto);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var metric = await _metricsRepository.GetByIdAsync(id);
        if (metric == null)
        {
            return NotFound();
        }
        return Ok(metric);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMetricsRequestDto metricsDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var metricsModel = metricsDto.ToMetricsFromCreateDto();
        await _metricsRepository.CreateAsync(metricsModel);
        return CreatedAtAction(nameof(GetById), new { id = metricsModel.Id }, metricsModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMetricsRequestDto metricsDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var metricsModel = await _metricsRepository.UpdateAsync(id, metricsDto);

        if (metricsModel == null)
        {
            return NotFound();
        }
        return Ok(metricsModel.ToMetircsDto());
    }
    
    
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var metricsModel = await _metricsRepository.DeleteAsync(id);
        if (metricsModel == null)
        {
            return NotFound();
        }

        return NoContent();

    }
    // // This action is for returning a view (MVC)
    // [HttpGet("index")]  // This distinguishes the action with the /api/metrics/index route
    // public IActionResult Index()
    // {
    //     return View();
    // }
}