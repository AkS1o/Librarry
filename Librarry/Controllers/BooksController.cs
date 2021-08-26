using Librarry.Data.Models;
using Librarry.Data.Models.ViewModels;
using Librarry.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allbooks = _booksService.GetAllBooks();
            return Ok(allbooks);
        }

        [HttpGet("get-by-id-book/{id}")]
        public IActionResult GetBookById(int id)
        {
            var _book = _booksService.GetBookById(id);

            if (_book != null)
                return Ok(_book);
            else
                return NotFound();
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM bookVM)
        {
            _booksService.AddBook(bookVM);
            return Ok();
        }

        [HttpPut("edit-book/{id}")]
        public IActionResult EditBook(int id, [FromBody] BookVM bookVM)
        {
            var _book = _booksService.EditBook(id, bookVM);
            return Ok(_book);
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _booksService.DeleteBook(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
