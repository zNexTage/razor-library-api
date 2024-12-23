using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;

namespace RazorLibrary.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IWriteBookService _writeBookService;

        public BookController(IWriteBookService writeBookService)
        {
            _writeBookService = writeBookService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(WriteBookDto book) {
            var result = await _writeBookService.Add(book);

            return Created(string.Empty, result);
        }
    }
}
