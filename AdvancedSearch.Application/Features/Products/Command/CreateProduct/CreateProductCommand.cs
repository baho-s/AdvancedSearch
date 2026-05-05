using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Products.Command.CreateProduct
{
    public record CreateProductCommand:IRequest<Guid>
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public string Information { get; init; }
        public string Features { get; init; }
        public string Description { get; init; }

        public Guid CategoryId { get; init; }
    }
}
