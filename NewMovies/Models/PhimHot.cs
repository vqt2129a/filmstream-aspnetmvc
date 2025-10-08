using System;
using System.Collections.Generic;

namespace NewMovies.Models;

public partial class PhimHot
{
    public int Id { get; set; }

    public string? Anh { get; set; }

    public string TenPhimTv { get; set; } = null!;

    public string? TenPhimEn { get; set; }

    public string? TheLoai { get; set; }

    public string? QuocGia { get; set; }

    public int? NamPhatHanh { get; set; }

    public int? LuotXem { get; set; }

    public string? TrangThai { get; set; }
}
