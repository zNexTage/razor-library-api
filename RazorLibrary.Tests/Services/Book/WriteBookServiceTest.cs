using RazorLibrary.Application.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.Tests.Services.Book
{
    public class WriteBookServiceTest
    {
        [Fact]
        public void Add_WithValidValue()
        {
            WriteBookDto bookDto = new WriteBookDto()
            {
                Authors = {
                    "Árthur Conan Doyle"
                },
                Photo = "https://jpimg.com.br/uploads/2018/06/sherlock.jpg",
                Publisher = "Harper Collins",
                Title = "Box Sherlock Holmes - Obra completa"
            };

            var service = new WriteBookService();
            
            service.Add(bookDto);
        }
    }
}
