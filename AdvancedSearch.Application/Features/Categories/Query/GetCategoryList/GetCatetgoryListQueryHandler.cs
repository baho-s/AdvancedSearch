using AdvancedSearch.Application.DTOs.CategoryDTOs;
using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CategoryName = c.CategoryName
            }).ToList();
        }
    }
}
