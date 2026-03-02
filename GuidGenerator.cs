namespace CustomMiddleware;

public class GuidGenerator : IGuidGenerator
{
    public string GetGuid() => Guid.NewGuid().ToString();
}
