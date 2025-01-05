using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using WebApplication3.Common;
using WebApplication3.Interfaces;

namespace WebApplication3.Services;

public class GlobalParameterService : IGlobalParameterService
{
    private readonly IOptions<GlobalSettings> _settings;
    private static readonly ConcurrentDictionary<string, object> _runtime = new();

    public GlobalParameterService(IOptions<GlobalSettings> settings)
    {
        _settings = settings;
    }

    public string GetSchemaName()
    {
        return GetParameter<string>("SchemaName") ?? _settings.Value.SchemaName;
    }

    public void SetSchemaName(string schema)
    {
        SetParameter("SchemaName", schema);
    }

    public T GetParameter<T>(string key)
    {
        return _runtime.TryGetValue(key, out var value) ? (T)value : default;
    }

    public void SetParameter<T>(string key, T value)
    {
        _runtime.AddOrUpdate(key, value, (_, _) => value);
    }
}