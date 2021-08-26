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
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _authorService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorService = authorsService;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllPublishers()
        {
            var allAuthors = _authorService.GetAllAuthors();
            return Ok(allAuthors);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _author = _authorService.GetAuthorById(id);

            if (_author != null)
                return Ok(_author);
            else
                return NotFound();
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM authorVM)
        {
            _authorService.AddAuthor(authorVM);
            return Created(nameof(AddAuthor), authorVM);
        }

        [HttpPut("edit-author/{id}")]
        public IActionResult EditAuthor(int id, [FromBody] AuthorVM authorVM)
        {
            var _author = _authorService.EditAuthor(id, authorVM);
            return Ok(_author);
        }

        [HttpDelete("delete-author/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
