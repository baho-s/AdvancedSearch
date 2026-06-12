using AdvancedSearch.Application.Features.Products.Command.CreateProduct;
using AdvancedSearch.Application.Features.Products.Query.SearchProducts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SemanticSearch(
            [FromQuery] string query,
            [FromQuery] int topK = 10, 
            [FromQuery] double minScore = 0.0,
            CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(query))
                return BadRequest("Arama metini boş olamaz.");

            var result= await _mediator.Send(new SearchProductsQuery(query, topK, minScore), cancellationToken);

            if (!result.Any())
                return Ok(new
                {
                    Message = "Arama kriterlerinize uygun ürün bulunamadı.",
                    Results = result
                });

            return Ok(new
            {
                Query = query,
                TotalFound = result.Count,
                Results = result
            });
        }
    }
}