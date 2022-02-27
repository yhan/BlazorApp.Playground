namespace BlazorApp.Playground.Data;

public class StateContainer
{
    private string? savedString;

    public string Property
    {
        get => savedString ?? string.Empty;
        set
        {
            savedString = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task RunAsync()
    {
        while (true)
        {
            await Task.Delay(1000);

            Property = $"Now {DateTime.Now}";
        }
    }
}