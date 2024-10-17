using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfluanceHairCare.models.Migrations
{
    public partial class intsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "SaleReps",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OrderPaidAmount",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPaidAmount",
                table: "Orders");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "SaleReps",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
