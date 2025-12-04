using Fiap.FCG.Game.Domain.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class ItemCompraConfiguration : IEntityTypeConfiguration<ItemCompra>
    {
        public void Configure(EntityTypeBuilder<ItemCompra> builder)
        {
            builder.ToTable("ItensCompra");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.JogoId)
                   .IsRequired();

            builder.Property(x => x.PrecoPago)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}
