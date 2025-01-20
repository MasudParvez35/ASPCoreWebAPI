using StackExchange.Redis;

namespace ASPCoreWebAPI.Cache;

public class ConnectionHelper
{
    private static Lazy<ConnectionMultiplexer> lazyConnection;
    static ConnectionHelper()
    {
        lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);
        });
    }

    public static ConnectionMultiplexer Connection
    {
        get
        {
            return lazyConnection.Value;
        }
    }
}
