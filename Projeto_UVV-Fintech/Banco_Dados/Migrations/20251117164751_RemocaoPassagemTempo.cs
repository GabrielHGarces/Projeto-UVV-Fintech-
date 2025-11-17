using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_UVV_Fintech.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoPassagemTempo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimaCobranca",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "UltimoRendimento",
                table: "Contas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaCobranca",
                table: "Contas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoRendimento",
                table: "Contas",
                type: "datetime2",
                nullable: true);
        }
    }
}
