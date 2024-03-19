namespace DbGateway;

public abstract class BaseDAO
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
