using Microsoft.AspNetCore.Mvc;
using RazorLibrary.Domain.Adapters.Services.Book;
using RazorLibrary.Domain.DataTransferObject.Book;
using RazorLibrary.Domain.Exception;

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

        /// <summary>
        /// Cria um novo livro na base de dados.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Livro criado</returns>
        [HttpPost]
        [EndpointSummary("Cria um novo livro na base de dados")]
        [ProducesResponseType(typeof(ReadBookDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Add(WriteBookDto book) {
            try
            {
                var result = await _writeBookService.Add(book);

                return Created(string.Empty, result);
            }
            catch(ValidationException err)
            {
                return BadRequest(err.ErrorMessages);
            }
            catch(Exception err)
            {
                return StatusCode(500, "Ops... Ocorreu um erro interno e não foi possível concluir a ação.");
            }

        }

        /// <summary>
        /// Obtém todos os livros registrados na base de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EndpointSummary("Obtém todos os livros registrados na base de dados")]
        [ProducesResponseType(typeof(List<ReadBookDto>), 200)]
        public async Task<ActionResult> GetAll()
        {
            var books = await _readBookService.GetAll();

            return Ok(books);
        }

        /// <summary>
        /// Obtém um livro na base de dados utilizando o id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [EndpointSummary("Obtém um livro na base de dados utilizando o id")]
        [ProducesResponseType(typeof(List<ReadBookDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var book = await _readBookService.GetById(id);

                return Ok(book);
            }
            catch (NotFoundException err)
            {
                return NotFound(err.Message);
            }
        }

        /// <summary>
        /// remove um livro da base de dados utilizando o id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [EndpointSummary("Remove um livro da base de dados utilizando o id")]
        [ProducesResponseType(typeof(ReadBookDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _writeBookService.Delete(id);

                return Ok();
            }
            catch (NotFoundException err) {
                return NotFound(err.Message);
            }
        }

        /// <summary>
        /// Atualiza um livro na base de dados
        /// </summary>
        /// <param name="id">Id do livro a ser atualizado</param>
        /// <param name="bookDto">Dados que serão utilizados para atualizar o livro</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um livro na base de dados")]
        [ProducesResponseType(typeof(ReadBookDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> Update([FromRoute]string id, [FromBody]WriteBookDto bookDto)
        {
            try
            {
                var book = await _writeBookService.Edit(id, bookDto);

                return Ok(book);
            }
            catch (NotFoundException err)
            {
                return NotFound(err.Message);
            }
            catch(ValidationException err)
            {
                return BadRequest(err.ErrorMessages);
            }
        }
    }
}
