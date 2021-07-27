using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Client.Controllers
{
    [ApiController] 
    // [Authorize]
    [Route("api/[controller]")]
    public class PostController : BaseApiController
    { 
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestParameter filter)
        {
            var res = await Mediator.Send(new GetAllPostsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
            return Ok(res);
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody]CreatePostCommand command)
        { 
            return Ok(await Mediator.Send(command));
        } 
    }
}
