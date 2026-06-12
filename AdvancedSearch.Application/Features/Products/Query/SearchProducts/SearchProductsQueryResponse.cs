using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Products.Query.SearchProducts
{
    public record SearchProductsQueryResponse(Guid Id,
    string Name,
    decimal Price,
    int Stock,
    string Description,
    string Features,
    double Score);
}
