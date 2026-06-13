using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //Primary keyi tanımladık
            builder.HasKey(c => c.Id);

            //EF Core'a Id'yi Db'de üretmeyeceğini kodda üreteceğimizi söylüyorz.
            builder.Property(c => c.Id)
                .ValueGeneratedNever();

        }
    }
}
