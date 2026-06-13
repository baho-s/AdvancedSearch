using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.ProductComment.Comamnd.CreateComment
{
    public record CreateCommentCommand:IRequest<Guid>
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
        public string CommentText { get; init; }
    }
}
