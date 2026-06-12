using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Categories.Query.SearchCategory
{
    public record SearchCategoryQueryResponse(Guid Id, string Name,double Score);
    
}
