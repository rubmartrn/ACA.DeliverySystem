
namespace ACA.DeliverySystem.Data
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

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>
            {
                Success = true,
                Data = data
            };
        }

        public static new OperationResult<T> Ok()
        {
            return new OperationResult<T>
            {
                Success = true
            };
        }

        public static new OperationResult<T> Error(string? message = null, ErrorType? errorType = null)
        {
            return new OperationResult<T>
            {
                Success = false,
                ErrorMessage = message,
                ErrorType = errorType
            };
        }
    }
}
