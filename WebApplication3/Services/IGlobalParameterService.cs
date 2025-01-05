namespace WebApplication3.Interfaces;

public interface IGlobalParameterService
{
    string GetSchemaName();
    void SetSchemaName(string schema);
    T GetParameter<T>(string key);
    void SetParameter<T>(string key, T value);
}
