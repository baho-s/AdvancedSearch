using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Categories.Command
{
    public record CreateCategoryCommand:IRequest<Guid>
    {

    }
}
