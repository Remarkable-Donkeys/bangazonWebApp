using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace bangazonWebApp.Migrations
{
    public partial class ProductChangedCategoryIdToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CategoryType_CategoryId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CategoryType_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "CategoryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CategoryType_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId1",
                table: "Product",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CategoryType_CategoryId1",
                table: "Product",
                column: "CategoryId1",
                principalTable: "CategoryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
