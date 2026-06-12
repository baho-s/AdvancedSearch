using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pgvector;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Embedding)
                .HasColumnType("vector(768)")
                .HasConversion(
                v => v == null ? null : new Vector(v),//v null ise null, değilse v'yi Vector'e dönüştür
                v => v == null ? null : v.ToArray()//v null ise null, değilse Vector'ü float[]'e dönüştür
            )
            .Metadata.SetValueComparer(new ValueComparer<float[]?>(
                (a, b) => a != null && b != null && a.SequenceEqual(b),//a ve b null değilse ve a ile b eşitse true, değilse false
                v => v.Aggregate(0, (a, b) => HashCode.Combine(a, b.GetHashCode())),//v null değilse v'nin her bir elemanının hash kodunu birleştirerek tek bir hash kodu oluştur
                v => v.ToArray() //v null değilse v'yi float[]'e dönüştür,v nedir?
                ));
            //v, embedding vektörünü temsil eder ve bu vektörün veritabanında nasıl saklanacağını belirler. Pgvector kütüphanesi,
            //vektör verilerini veritabanında saklamak için özel bir tür sağlar. Bu yapılandırma, Category sınıfındaki Embedding
            //özelliğinin Pgvector kütüphanesinin Vector türü olarak saklanmasını sağlar. Ayrıca, ValueComparer kullanarak
            //iki float[] dizisinin eşit olup olmadığını kontrol eder ve hash kodu oluşturur.

        }
    }
}
