namespace RazorLibrary.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public string Authors { get; set; } = string.Empty;
    }
}
