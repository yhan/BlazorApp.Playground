using System.Buffers;
using System.Diagnostics.Tracing;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Playground.Data;

public class FlightRepository
{
    public async Task<int> Count()
    {
        await using var dbContext = new FlightsDbContext();
        return await dbContext.Flights.AsNoTracking().CountAsync();
    }

    public async Task<Flight[]> GetChunk(int startIdx, int chunkSize,
        Flight[] pooled,
        CancellationToken ct)
    {
        await using var dbContext = new FlightsDbContext();
        var queryable = dbContext.Flights.AsNoTracking().Skip(startIdx)
            .Take(chunkSize);

        int i = 0;
        foreach (Flight f in queryable)
        {
            pooled[i] = f;
            i++;
        }

        return pooled;
    }

    public async Task<Flight[]> GetChunk_NoPool(int startIdx, int chunkSize, CancellationToken ct)
    {
        await using var dbContext = new FlightsDbContext();
        return await dbContext.Flights.AsNoTracking().Skip(startIdx)
            .Take(chunkSize).ToArrayAsync(cancellationToken: ct);
    }

    public async Task Init()
    {
        await using var dbContext = new FlightsDbContext();
        await dbContext.Database.ExecuteSqlRawAsync(@"truncate table hello.""Flights""");
        await dbContext.BulkInsertAsync(Enumerable.Range(1, 10_000).Select(FlightBuilder.Build).ToList());
    }
}