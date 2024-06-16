using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstocariaNet.Migrations
{
    /// <inheritdoc />
    public partial class mg_modfyRelatorios02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalArrecadado",
                table: "Relatorios",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Relatorios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Relatorios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MesAnoPred",
                table: "Relatorios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PredProdutoEntrada",
                table: "Relatorios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PredProdutoSaida",
                table: "Relatorios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PredTotalArrecadar",
                table: "Relatorios",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PredicaoProxMeses",
                table: "Relatorios",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoMaisEntrou",
                table: "Relatorios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoMaisSaiu",
                table: "Relatorios",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "MesAnoPred",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PredProdutoEntrada",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PredProdutoSaida",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PredTotalArrecadar",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PredicaoProxMeses",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "ProdutoMaisEntrou",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "ProdutoMaisSaiu",
                table: "Relatorios");

            migrationBuilder.AlterColumn<double>(
                name: "TotalArrecadado",
                table: "Relatorios",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
