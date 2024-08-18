﻿namespace ACA.DeliverySystem.Data
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public ErrorType? ErrorType { get; set; }

        public static OperationResult Ok()
        {
            return new OperationResult()
            {
                Success = true
            };
        }

        public static OperationResult Error(string? message = null, ErrorType? errorType = null)
        {
            return new OperationResult()
            {
                Success = false,
                ErrorMessage = message,
                ErrorType = errorType
            };
        }
    }

    public enum ErrorType
    {
        NotFound,            // Resource or data not found
        NetworkError,        // Issues with network connectivity
        ValidationError,     // Data validation failed
        Unauthorized,        // Unauthorized access attempt
        Forbidden,           // User does not have the necessary permissions
        Timeout,             // Operation timed out
        Conflict,            // Conflicting data or resource states
        InternalServerError, // Generic server error
        BadRequest,          // Invalid request parameters or data
        ServiceUnavailable,  // External service is unavailable
        DatabaseError,       // Issues with database operations
        DependencyError,     // Failure due to external dependency
        InvalidOperation,    // Operation is not allowed in the current context
        UnknownError         // Fallback for unexpected errors
    }
}
