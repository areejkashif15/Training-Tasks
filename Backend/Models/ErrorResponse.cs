namespace ProductManagement.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; }

        public string? Path { get; set; }

        public string? CorrelationId { get; set; }

        public ErrorResponse(
            int statusCode, 
            string error, 
            string message, 
            string? details = null, 
            string? path = null,
            string? correlationId = null)
        {
            StatusCode = statusCode;
            Error = error;
            Message = message;
            Details = details;
            Timestamp = DateTime.UtcNow;
            Path = path;
            CorrelationId = correlationId;
        }
    }
}