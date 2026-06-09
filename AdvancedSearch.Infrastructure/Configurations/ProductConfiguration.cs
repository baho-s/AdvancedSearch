using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pgvector;
using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Embedding)
                .HasColumnType("vector(1536)")
                .HasConversion(
                    v => v == null ? null : new Vector(v),     // float[] → Vector
                    v => v == null ? null : v.ToArray()         // Vector → float[]
            );
        }
    }
}
