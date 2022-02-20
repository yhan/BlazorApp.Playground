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