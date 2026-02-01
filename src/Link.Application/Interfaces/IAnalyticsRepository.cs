using Link.Domain.Entities;

namespace Link.Application.Interfaces;

public interface IAnalyticsRepository
{
    Task BulkInsertClicksAsync(List<ClickEvent> events);
}