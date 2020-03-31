using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contacts.Migrations
{
    public partial class AddedProfilePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilePhotoId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfilePhotos",
                columns: table => new
                {
                    ProfilePhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhotos", x => x.ProfilePhotoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ProfilePhotoId",
                table: "Contacts",
                column: "ProfilePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ProfilePhotos_ProfilePhotoId",
                table: "Contacts",
                column: "ProfilePhotoId",
                principalTable: "ProfilePhotos",
                principalColumn: "ProfilePhotoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ProfilePhotos_ProfilePhotoId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ProfilePhotoId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoId",
                table: "Contacts");
        }
    }
}
