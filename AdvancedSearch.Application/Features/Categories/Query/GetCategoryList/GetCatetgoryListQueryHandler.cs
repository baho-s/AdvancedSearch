using AdvancedSearch.Application.DTOs.CategoryDTOs;
using AdvancedSearch.Application.Interfaces.Persistence;
using MediatR;

namespace AdvancedSearch.Application.Features.Categories.Query.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
            
        public async Task<List<CategoryListDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(cancellationToken);
            return categories.Select(c => new CategoryListDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            }).ToList();
        }
    }
}
