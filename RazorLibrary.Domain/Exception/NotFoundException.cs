namespace RazorLibrary.Domain.Exception
{
    public class NotFoundException(string message) : BaseRazorLibraryException(message)
    {
    }
}
