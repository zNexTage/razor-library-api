namespace RazorLibrary.Domain.Exception
{
    public class ValidationException(Dictionary<string, string> errorMessages) : SystemException
    {
        public readonly Dictionary<string, string> ErrorMessages = errorMessages;
    }
}
