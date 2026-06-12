using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Products.Query.SearchProducts
{
    public record SearchProductsQuery(string UserText, int TopK = 10, double MinScore = 0.70) : IRequest<List<SearchProductsQueryResponse>>;

}

