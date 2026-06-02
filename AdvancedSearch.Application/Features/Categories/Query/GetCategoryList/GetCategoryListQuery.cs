using AdvancedSearch.Application.DTOs.CategoryDTOs;
using MediatR;

namespace AdvancedSearch.Application.Features.Categories.Query.GetCategoryList
{
    public record class GetCategoryListQuery:IRequest<List<CategoryListDto>>
    {

    }
}
