using System;
using System.Collections.Generic;

namespace NewMovies.Models;

public partial class QuanLyTaiKhoan
{
    public int Id { get; set; }

    public string TaiKhoan { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string VaiTro { get; set; } = null!;

    public DateTime? NgayDangKy { get; set; }

    public string? TrangThai { get; set; }
}
