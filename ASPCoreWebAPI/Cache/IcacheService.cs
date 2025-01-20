namespace ASPCoreWebAPI.Cache;

public interface IcacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T data, DateTimeOffset expirationTime);
    object RemoveData(string key);
}
