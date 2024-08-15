namespace ACA.DeliverySystem.UI.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }   

        public string?  ErrorMessage { get; set; }

        public static OperationResult Ok() 
        {
            return new OperationResult { Success = true };
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult { Success = false, ErrorMessage = message};
        }


    }
}
