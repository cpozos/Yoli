namespace Yoli.WebApi.Resolvers;

public interface ICustomProcessor
{
    void Process();
}
public class CustomProcessor1 : ICustomProcessor
{
    public void Process()
    {
        throw new NotImplementedException();
    }
}

public class CustomProcessor2 : ICustomProcessor
{
    public void Process()
    {
        throw new NotImplementedException();
    }
}

public class CustomProcessorResolver
{
    private readonly CustomProcessor1 _customProcessor1;
    private readonly CustomProcessor2 _customProcessor2;

    public CustomProcessorResolver(CustomProcessor1 customProcessor1, CustomProcessor2 customProcessor2)
    {
        _customProcessor1 = customProcessor1;
        _customProcessor2 = customProcessor2;
    }

    public ICustomProcessor GetProcessor(string name)
        => name switch
        {
            nameof(CustomProcessor1) => _customProcessor1,
            nameof(CustomProcessor2) => _customProcessor2,
            _ => throw new ArgumentOutOfRangeException(nameof(name), ""),
        };
}

public delegate ICustomProcessor CustomProcessorResolver2(string processorName);