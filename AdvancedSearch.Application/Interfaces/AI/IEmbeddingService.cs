using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Interfaces.AI
{
    public interface IEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string text);

    }
}
