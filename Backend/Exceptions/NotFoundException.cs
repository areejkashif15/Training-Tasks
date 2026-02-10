namespace ProductManagement.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} with id '{key}' was not found")
        {
        }
    }
}