namespace RazorLibrary.Domain.Exception
{
    public class BaseRazorLibraryException(string message) : SystemException(message)
    {
    }
}
