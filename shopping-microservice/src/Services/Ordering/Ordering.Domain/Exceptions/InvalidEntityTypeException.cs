namespace Ordering.Domain.Exceptions
{
    internal class InvalidEntityTypeException : ApplicationException
    {
        public InvalidEntityTypeException(string entity, string type) :
            base($"Entity \"{entity}\" not support type: {type}")
        {
            
        }
    }
}
