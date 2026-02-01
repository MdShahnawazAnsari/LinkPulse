using Link.Application.Interfaces;
using Link.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Link.Infrastructure.Persistence.Repositories;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly string _connectionString;

    public AnalyticsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task BulkInsertClicksAsync(List<ClickEvent> events)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        // BINARY COPY: The fastest way to insert data into Postgres
        using var writer = await conn.BeginBinaryImportAsync(
            "COPY analytics (short_code, clicked_at, ip_address, user_agent) FROM STDIN (FORMAT BINARY)");

        foreach (var evt in events)
        {
            await writer.StartRowAsync();
            await writer.WriteAsync(evt.Shortcode);
            await writer.WriteAsync(evt.ClickedAt);
            await writer.WriteAsync(evt.IpAddress);
            await writer.WriteAsync(evt.UserAgent);
        }

        await writer.CompleteAsync();
    }
}