using AdvancedSearch.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Services
{
    public class FakeCurrentUserService:ICurrentUserService
    {
        public Guid UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");
    }
}
