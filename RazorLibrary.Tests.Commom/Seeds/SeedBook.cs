using Microsoft.EntityFrameworkCore;
using RazorLibrary.Domain.Entities;
using RazorLibrary.Infra.Database;

namespace RazorLibrary.Tests.Commom.Seeds
{
    public class SeedBook
    {
        private readonly BookContext _context;

        public SeedBook(BookContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            var books = new List<Book> {
                new Book
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "The Great Gatsby",
                    Publisher = "Scribner",
                    Photo = "great-gatsby.jpg",
                    Authors = "F. Scott Fitzgerald"
                },
                new Book
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "1984",
                    Publisher = "Secker & Warburg",
                    Photo = "1984.jpg",
                    Authors = "George Orwell"
                },
                new Book
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "To Kill a Mockingbird",
                    Publisher = "J.B. Lippincott & Co.",
                    Photo = "to-kill-a-mockingbird.jpg",
                    Authors = "Harper Lee"
                },
                new Book
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Moby-Dick",
                    Publisher = "Harper & Brothers",
                    Photo = "moby-dick.jpg",
                    Authors = "Herman Melville"
                },
                new Book
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "Pride and Prejudice",
                    Publisher = "T. Egerton",
                    Photo = "pride-and-prejudice.jpg",
                    Authors = "Jane Austen"
                }
            };

            var alreadyRegistered = await _context.Books.AnyAsync();

            if (alreadyRegistered) return;

            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
        }
    }
}
