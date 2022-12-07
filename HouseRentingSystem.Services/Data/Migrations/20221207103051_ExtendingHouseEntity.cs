using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Services.Data.Migrations
{
    public partial class ExtendingHouseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9421431c-13f1-441a-a0a3-462bc5cae510", "AQAAAAEAACcQAAAAEMQxHPaTs5PLn+SKl9F0JhpDultVatMcafAgjXokQPfiWBFtX6Pk3ELnKDqCbb3/Og==", "ec05ebd2-589d-42cd-ab0e-8a2ea6d0fc8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c078ae28-a243-4c96-a49d-17bfe8de4d64", "AQAAAAEAACcQAAAAEARRg0fq4vnouALdjUheRmEObf3LQBMIgdMJ6s8EB2SOmunBFlAeNsecIGx4VwLW9Q==", "11a3da8c-ca3f-44eb-9083-d88c74ac3695" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63b0d5b7-738f-4dac-8191-728bfc392b80", "AQAAAAEAACcQAAAAEH4/z64zyEWXkGxzM+OfSLRWgQiG2JcgjKFx4F4z8IsPLJL4uf9aZnJ2r2psYP1j6g==", "0b70cf39-c50f-491a-918f-be3f798984f4" });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_RenterId",
                table: "Houses",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_RenterId",
                table: "Houses");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b6d1d16-4d6a-4923-8548-806980837372", "AQAAAAEAACcQAAAAEK85gxKbqr5Vmz5aYWsY9oMLVZVP4OBDiCDp6p2Zd21w2Hx8MVcf5jAHr6Cbvhg5sw==", "2df676a2-ffbc-4dc2-96de-405daa430fd3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae647c47-b266-4dda-a9b7-1b3393296d42", "AQAAAAEAACcQAAAAEDD3E/LB5MJroErXRIo0R1gaQ8qMR9qk872dAFtuTYBfwhMmYCInDFK/NKX+5EpsXw==", "c22de16f-063b-418a-b1c2-b96bf7e069b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2999cf2e-6def-4dc5-b910-cad3d1dd8a5b", "AQAAAAEAACcQAAAAEJeBd7eqv2Z66v2RP7UrfjvBDfIPcWQ8DtKWSggxiECkM49NZz/gk4R48TFn/THg6Q==", "3d2408ba-c222-46bb-b1e4-45b6bf36923e" });
        }
    }
}
