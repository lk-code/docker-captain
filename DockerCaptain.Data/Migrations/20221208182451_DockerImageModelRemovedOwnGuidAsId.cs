using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DockerCaptain.Data.Migrations
{
    /// <inheritdoc />
    public partial class DockerImageModelRemovedOwnGuidAsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "app",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "app",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                schema: "app",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Container",
                schema: "app",
                newName: "Container");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "DockerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Images",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "Container",
                newName: "Container",
                newSchema: "app");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "app",
                table: "Images",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "app",
                table: "Images",
                column: "Id");
        }
    }
}
