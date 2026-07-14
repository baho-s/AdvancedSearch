using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            // ProductCategory tablosundaki Product ilişkisini yapılandırıyoruz.
            builder.HasOne(pc => pc.Product) // Her ProductCategory kaydı bir Product'a bağlıdır.
                .WithMany(p => p.ProductCategories) // Bir Product birden fazla ProductCategory ilişkisine sahip olabilir.
                .HasForeignKey(pc => pc.ProductId) // Foreign key olarak ProductId kullanılacak.
                .OnDelete(DeleteBehavior.Cascade); // Product silinirse ona bağlı ProductCategory kayıtları da silinir.

            // ProductCategory tablosundaki Category ilişkisini yapılandırıyoruz.
            builder.HasOne(pc => pc.Category) // Her ProductCategory kaydı bir Category'ye bağlıdır.
                .WithMany(c => c.ProductCategories) // Bir Category birden fazla ProductCategory ilişkisine sahip olabilir.
                .HasForeignKey(pc => pc.CategoryId) // Foreign key olarak CategoryId kullanılacak.
                .OnDelete(DeleteBehavior.Cascade); // Category silinirse ona bağlı ProductCategory kayıtları da silinir.

            // Aynı ürünün aynı kategoriye aktif olarak birden fazla kez bağlanmasını engelliyoruz.
            builder.HasIndex(pc => new { pc.ProductId, pc.CategoryId }) // ProductId + CategoryId birlikte indexlenir.
                .IsUnique() // Aynı ProductId + CategoryId kombinasyonu tekrar edemez.
                .HasFilter("\"IsDeleted\" = false"); // Sadece IsDeleted = false olan aktif kayıtlar için unique kuralı uygulanır.
        }
    }
}
