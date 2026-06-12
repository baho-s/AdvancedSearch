using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            builder.Property(p => p.Embedding)
                .HasColumnType("vector(768)")
                .HasConversion(
                v => v == null ? null : new Vector(v),
                v => v == null ? null : v.ToArray()
            )
            .Metadata.SetValueComparer(new ValueComparer<float[]>(
                (a, b) => a != null && b != null && a.SequenceEqual(b), // eşitlik kontrolü
                v => v.Aggregate(0, (a, b) => HashCode.Combine(a, b.GetHashCode())), // hash
                v => v.ToArray() // snapshot
            ));
        }
    }
}
