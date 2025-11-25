using Fiap.FCG.Game.Domain.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.FCG.Game.Infrastructure.Notificacoes;

public class NotificacaoEnviadaConfig : IEntityTypeConfiguration<NotificacaoEnviada>
{
    public void Configure(EntityTypeBuilder<NotificacaoEnviada> builder)
    {
        builder.ToTable("notificacao_enviada");

        builder.HasKey(ne => ne.Id);

        builder.Property(ne => ne.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(ne => ne.PromocaoJogo)
            .WithMany()
            .HasForeignKey(ne => ne.PromocaoJogoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ne => ne.Notificacao)
            .WithMany(n => n.Enviadas) 
            .HasForeignKey(ne => ne.NotificacaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}