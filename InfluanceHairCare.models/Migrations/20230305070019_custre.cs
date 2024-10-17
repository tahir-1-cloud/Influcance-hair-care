using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfluanceHairCare.models.Migrations
{
    public partial class custre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "SaleRepsOrderPayments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChequeExpiryDate",
                table: "SaleRepsOrderPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ChequeFor",
                table: "SaleRepsOrderPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChequeNumber",
                table: "SaleRepsOrderPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SaleRepOrderTimeAmount",
                table: "SaleRepsOrderPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChequeExpiryDate",
                table: "SaleRepsOrderPayments");

            migrationBuilder.DropColumn(
                name: "ChequeFor",
                table: "SaleRepsOrderPayments");

            migrationBuilder.DropColumn(
                name: "ChequeNumber",
                table: "SaleRepsOrderPayments");

            migrationBuilder.DropColumn(
                name: "SaleRepOrderTimeAmount",
                table: "SaleRepsOrderPayments");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "SaleRepsOrderPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
