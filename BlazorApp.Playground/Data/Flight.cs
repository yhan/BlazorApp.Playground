namespace BlazorApp.Playground.Data;

public class Flight
{
    public Flight(int flightId, string summary)
    {
        FlightId = flightId;
        Summary = summary;
    }

    public int FlightId { get; }
    public string Summary { get; set; }
}

public class FlightService
{
    public static Flight[] allFlights = Enumerable.Range(1, 10_000).Select(FlightBuilder.Build).ToArray();


    public static async Task<IEnumerable<Flight>> GetChunk(int startIdx, int chunkSize, CancellationToken ct)
    {
        return new ArraySegment<Flight>(allFlights, startIdx, chunkSize);
    }
}

public class FlightBuilder
{
    private static Random random = new Random();

    public static Flight Build(int id)
    {
        return new Flight(id, RandomString(10));
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}