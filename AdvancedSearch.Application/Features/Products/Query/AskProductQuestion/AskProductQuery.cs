using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Products.Query.AskProductQuestion
{
    public record AskProductQuery(string Question) : IRequest<AskProductQueryResponse>;
}
