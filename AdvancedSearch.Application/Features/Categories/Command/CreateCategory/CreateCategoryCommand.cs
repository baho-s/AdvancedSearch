using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Categories.Command.CreateCategory
{
    public record CreateCategoryCommand:IRequest<Guid>
    {
        public string CategoryName { get; init; }
    }
}
