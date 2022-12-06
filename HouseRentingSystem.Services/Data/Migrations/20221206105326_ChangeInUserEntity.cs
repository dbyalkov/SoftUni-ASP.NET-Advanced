using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Services.Data.Migrations
{
    public partial class ChangeInUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a746620c-e0bb-4a41-990d-52604aeb82f0", "AQAAAAEAACcQAAAAEBSlD+f8yK+yj2kq+JM3hE8qMdrXd1e72Q40bnISxgGKas2gQ2cSRe+mcPQ/L2GdWw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bf19553-22dc-4c65-9010-b83f7d8192ec", "AQAAAAEAACcQAAAAEBpdAIhHEUEbNMZg8zIv+tIbeM+Qb0OkLns8T8vsKHHah4D3F4zeFi7EJkzFtXlGAw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c328e1a3-fe78-4a24-b66a-f37abaa4a8bb", "AQAAAAEAACcQAAAAEKje5NYncVdk188yFNOo+IGNx9wXACWFpLzjiTvnO4cYn2JR9gE6vzIhvbni/chGMg==", null });
        }
    }
}
