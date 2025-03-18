using Worthy_API.Dtos.Metrics;
using Worthy_API.Models;

namespace Worthy_API.Mappers;

public static class MetricsMapper
{
    public static MetricsDto ToMetircsDto(this Metrics metrics)
    {
        return new MetricsDto
        {
            Id = metrics.Id,
            Name = metrics.Name,
            Type = metrics.Type,
            Score = metrics.Score,
        };
    }

    public static Metrics  ToMetricsFromCreateDto(this CreateMetricsRequestDto createmetricsDto)
    {
        return new Metrics
        {
            Name = createmetricsDto.Name,
            Type = createmetricsDto.Type,
            Score = createmetricsDto.Score,
        };
    }
}