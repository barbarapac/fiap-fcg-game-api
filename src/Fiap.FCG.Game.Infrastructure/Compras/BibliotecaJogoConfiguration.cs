using Fiap.FCG.Game.Domain.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Game.Infrastructure.Compras
{
    public class BibliotecaJogoConfiguration : IEntityTypeConfiguration<BibliotecaJogo>
    {
        public void Configure(EntityTypeBuilder<BibliotecaJogo> builder)
        {
            builder.ToTable("BibliotecaJogos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.JogoId)
                .IsRequired();

            builder.Property(x => x.DataAquisicao)
                .IsRequired();

            builder.HasOne(x => x.Jogo)
                .WithMany()
                .HasForeignKey(x => x.JogoId);

            builder.HasIndex(x => new { x.UsuarioId, x.JogoId })
                .IsUnique();
        }
    }
}
