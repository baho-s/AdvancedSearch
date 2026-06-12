using AdvancedSearch.Application.Features.Categories.Command.CreateCategory;
using AdvancedSearch.Application.Features.Categories.Query.GetCategoryList;
using AdvancedSearch.Application.Features.Categories.Query.SearchCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AdvancedSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoryListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("embedding")]
        public async Task<IActionResult> CreateCategoryEmbedding([FromQuery] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SemanticSearch([FromQuery] string userText, [FromQuery] int topK = 10, [FromQuery] double minScore = 0.70, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new SearchCategoryQuery(userText, topK, minScore), cancellationToken);
            return Ok(result);
        }
    }
}
