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
    public class PublishersController : ControllerBase
    {
        public readonly PublisherService _publisherService;

        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            var allPublishers = _publisherService.GetAllPublishers(sortBy, searchString, pageNumber);
            return Ok(allPublishers);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _publisher = _publisherService.GetPublisherById(id);

            if (_publisher != null)
                return Ok(_publisher);
            else
                return NotFound();
        }

        [HttpGet("get-publisher-book-with-author/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _publisherData = _publisherService.GetPublisherById(id);

            if (_publisherData != null)
                return Ok(_publisherData);
            else
                return NotFound();
        }


        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisherVM)
        {
            var publisher = _publisherService.AddPublisher(publisherVM);
            return Created(nameof(AddPublisher), publisher);
        }

        [HttpPut("edit-publisher/{id}")]
        public IActionResult EditPublisher(int id, [FromBody] PublisherVM publisherVM)
        {
            var _publisher = _publisherService.EditPublisher(id, publisherVM);
            return Ok(_publisher);
        }

        [HttpDelete("delete-publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                _publisherService.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
