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
        private IReadBookService _readBookService;

        public BookController(IWriteBookService writeBookService, 
            IReadBookService readBookService)
        {
            _writeBookService = writeBookService;
            _readBookService = readBookService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(WriteBookDto book) {
            var result = await _writeBookService.Add(book);

            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var books = await _readBookService.GetAll();

            return Ok(books);
        }
    }
}
