using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.FCG.Game.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComprasRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItensCompra_JogoId",
                table: "ItensCompra",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaJogos_JogoId",
                table: "BibliotecaJogos",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BibliotecaJogos_jogo_JogoId",
                table: "BibliotecaJogos",
                column: "JogoId",
                principalTable: "jogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompra_jogo_JogoId",
                table: "ItensCompra",
                column: "JogoId",
                principalTable: "jogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibliotecaJogos_jogo_JogoId",
                table: "BibliotecaJogos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompra_jogo_JogoId",
                table: "ItensCompra");

            migrationBuilder.DropIndex(
                name: "IX_ItensCompra_JogoId",
                table: "ItensCompra");

            migrationBuilder.DropIndex(
                name: "IX_BibliotecaJogos_JogoId",
                table: "BibliotecaJogos");
        }
    }
}
