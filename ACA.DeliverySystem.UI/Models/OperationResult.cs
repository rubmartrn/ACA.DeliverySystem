namespace ACA.DeliverySystem.UI.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public static OperationResult Ok()
        {
            return new OperationResult { Success = true };
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult { Success = false, ErrorMessage = message };
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

        public static new OperationResult<T> Fail(string? message = null)
        {
            return new OperationResult<T>
            {
                Success = false,
                ErrorMessage = message,
            };
        }
    }
}
