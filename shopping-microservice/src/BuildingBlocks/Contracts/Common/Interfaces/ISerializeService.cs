namespace Contracts.Common.Interfaces
{
    public interface ISerializeService
    {
        string Serialize<T>(T obj);
        
        string Serialize<T>(T obj, Type type);

        T Deserilize<T>(string text);
    }
}
