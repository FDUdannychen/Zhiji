using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zhiji.IntegrationEventLog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntegrationEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Arguments = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PublishTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEvents");
        }
    }
}
