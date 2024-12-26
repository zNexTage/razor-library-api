﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
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

        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var books = await _readBookService.GetAll();

            return Ok(books);
        }

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]string id, [FromBody]WriteBookDto bookDto)
        {
            try
            {
                await _writeBookService.Edit(id, bookDto);

                return Ok();
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
