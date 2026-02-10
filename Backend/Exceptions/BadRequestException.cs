namespace ProductManagement.Exceptions
{
    public class BadRequestException : AppException
    {
        public string? ParameterName { get; }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }
    }
}