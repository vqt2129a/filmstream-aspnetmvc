using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewMovies.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNgayDangKyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhimBo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TenPhimTV = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TenPhimEN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TheLoai = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    QuocGia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NamPhatHanh = table.Column<int>(type: "int", nullable: true),
                    SoTap = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhimBo__3214EC2705E7AE88", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhimHot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TenPhimTV = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TenPhimEN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TheLoai = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    QuocGia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NamPhatHanh = table.Column<int>(type: "int", nullable: true),
                    LuotXem = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    TrangThai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhimHot__3214EC27D3B8D68B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhimLe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TenPhimTV = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TenPhimEN = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TheLoai = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    QuocGia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NamPhatHanh = table.Column<int>(type: "int", nullable: true),
                    ThoiLuong = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhimLe__3214EC277DE371E6", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuanLyTaiKhoan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDangKy = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuanLyTa__3214EC27AA837095", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__QuanLyTa__A9D10534B5D71F70",
                table: "QuanLyTaiKhoan",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhimBo");

            migrationBuilder.DropTable(
                name: "PhimHot");

            migrationBuilder.DropTable(
                name: "PhimLe");

            migrationBuilder.DropTable(
                name: "QuanLyTaiKhoan");
        }
    }
}
