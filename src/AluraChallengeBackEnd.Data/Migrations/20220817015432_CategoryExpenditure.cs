using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluraChallengeBackEnd.Data.Migrations
{
    public partial class CategoryExpenditure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Incomes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Expenditures",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryExpenditureId",
                table: "Expenditures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CategoriesExpenditure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesExpenditure", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_CategoryExpenditureId",
                table: "Expenditures",
                column: "CategoryExpenditureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_CategoriesExpenditure_CategoryExpenditureId",
                table: "Expenditures",
                column: "CategoryExpenditureId",
                principalTable: "CategoriesExpenditure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_CategoriesExpenditure_CategoryExpenditureId",
                table: "Expenditures");

            migrationBuilder.DropTable(
                name: "CategoriesExpenditure");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_CategoryExpenditureId",
                table: "Expenditures");

            migrationBuilder.DropColumn(
                name: "CategoryExpenditureId",
                table: "Expenditures");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Expenditures",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
