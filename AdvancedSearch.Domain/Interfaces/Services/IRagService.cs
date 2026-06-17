using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Domain.Interfaces.Services
{
    public interface IRagService
    {
        Task<string> GenerateAnswerAsync(string userQuestion, List<Product> products, CancellationToken cancellationToken);
    }
}
