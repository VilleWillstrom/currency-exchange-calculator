﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_exchange_calculator.Migrations
{
    /// <inheritdoc />
    public partial class AddConversionRateToCurrencyConversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "CurrencyConversions",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "CurrencyConversions");
        }
    }
}
