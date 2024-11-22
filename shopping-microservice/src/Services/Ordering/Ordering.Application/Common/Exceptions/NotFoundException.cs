namespace Ordering.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base()
        {
            
        }

        public NotFoundException(string messsage) : base(messsage) 
        {
            
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) 
        {
            
        }

        public NotFoundException(string name, object key) 
            : base($"Entity \"{name}\" ({key}) was not found")
        {
            
        }
    }
}
