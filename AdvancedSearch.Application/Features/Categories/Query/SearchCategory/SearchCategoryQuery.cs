using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Categories.Query.SearchCategory
{
    public record SearchCategoryQuery:IRequest<List<SearchCategoryQueryResponse>>
    {
        public string UserText { get; init; }
        public int TopK { get; init; } 
        public double MinScore { get; init; }
    
        public SearchCategoryQuery(string userText, int topK = 10, double minScore = 0.70)
        {
            UserText = userText;
            TopK = topK;
            MinScore = minScore;
        }
    }
}
