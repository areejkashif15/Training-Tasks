namespace ProductManagement.Exceptions
{
    /// <summary>
    /// Base exception for all application-specific exceptions
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}