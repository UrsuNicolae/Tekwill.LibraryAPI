using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForiginKeyForChatNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_notifications_chats_NotificationId",
                table: "chat_notifications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "books",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 20, 16, 3, 8, 513, DateTimeKind.Utc).AddTicks(8156),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2025, 8, 18, 17, 28, 11, 843, DateTimeKind.Utc).AddTicks(5802));

            migrationBuilder.AddForeignKey(
                name: "FK_chat_notifications_chats_ChatId",
                table: "chat_notifications",
                column: "ChatId",
                principalTable: "chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_notifications_chats_ChatId",
                table: "chat_notifications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "books",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 18, 17, 28, 11, 843, DateTimeKind.Utc).AddTicks(5802),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2025, 8, 20, 16, 3, 8, 513, DateTimeKind.Utc).AddTicks(8156));

            migrationBuilder.AddForeignKey(
                name: "FK_chat_notifications_chats_NotificationId",
                table: "chat_notifications",
                column: "NotificationId",
                principalTable: "chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
