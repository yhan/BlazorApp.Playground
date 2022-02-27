namespace WebAPI.Playground;

public class SharedMemory
{
    public List<long> Values = new();
    public void Add(long v)
    {
        Values.Add(v);
    }
}