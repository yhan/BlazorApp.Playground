using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlazorApp.Playground.Data;

public class FlightsDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }

    //public FlightsDbContext(DbContextOptions<FlightsDbContext>  options) : base(options)
    //{
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var conn = new NpgsqlConnection(
            "Server=127.0.0.1;Port=5432;Database=dbEfCore;User Id=postgres;Password=postgres;CommandTimeout=200;SearchPath=hello;");//CommandTimeout=200;
        conn.StateChange += (sender, e) =>
        {
            Debug.WriteLine($"CNX origin ={e.OriginalState} new={e.CurrentState}");
        };

        options.UseNpgsql(conn)
            .LogTo(StdOut)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(f => f.HasKey(f => f.FlightId));
    }

    void StdOut(string obj)
    {
        Debug.WriteLine(obj);
    }
}