using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CurrentAccountBalance = table.Column<decimal>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    AccountNumberGenerated = table.Column<string>(nullable: true),
                    PinHash = table.Column<byte[]>(nullable: true),
                    PinSalt = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionUniqueReference = table.Column<string>(nullable: true),
                    TransactionAmount = table.Column<decimal>(nullable: false),
                    TransactionStatus = table.Column<int>(nullable: false),
                    TransactionSourceAccount = table.Column<string>(nullable: true),
                    TransactionDestinationAccount = table.Column<string>(nullable: true),
                    TransactionParticulars = table.Column<string>(nullable: true),
                    TransactionType = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
