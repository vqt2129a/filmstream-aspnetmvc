using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewMovies.Models
{
    public class Movie
    {
        public string? Name { get; set; }       // Tên phim cho phép nullable
        public string? Origin_Name { get; set; } // Tên gốc
        public string? Thumb_Url { get; set; }  // Ảnh thu nhỏ
        public string? Poster_Url { get; set; } // Ảnh poster
        public int? Year { get; set; }          // Năm phát hành
        public string? Slug { get; set; } // Đường dẫn
        public string? Type { get; set; } // Loại phim
    }

    
    public class MovieDetailResponse
    {
        public bool Status { get; set; }
        public string Msg { get; set; } = string.Empty;
        public MovieDetail? Movie { get; set; }
        public List<Episode>? Episodes { get; set; }
    }


    
    public class MovieDetail
    {
        public string? Name { get; set; }
        public string? Origin_Name { get; set; }
        public string? Thumb_Url { get; set; }
        public string? Poster_Url { get; set; }
        public int? Year { get; set; }
        public string? Content { get; set; }
        public string? Time { get; set; }
        public string? Episode_Current { get; set; }
        public string? Episode_Total { get; set; }
        public string? Quality { get; set; }
        public string? Lang { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Trailer_Url { get; set; }
        public string? Notify { get; set; }
        public string? Showtimes { get; set; }
        public int? View { get; set; }
        public bool? Is_Copyright { get; set; }
        public bool? Chieurap { get; set; }
        public bool? Sub_Docquyen { get; set; }
        public string? Slug { get; set; }
        public List<string>? Actor { get; set; }
        public List<string>? Director { get; set; }
        public List<Category>? Category { get; set; }
        public List<Country>? Country { get; set; }
        public List<Episode>? Episodes { get; set; }
    }

    public class Episode
    {
        public string Server_Name { get; set; } = string.Empty;
        public List<ServerData>? Server_Data { get; set; }
    }

    public class ServerData
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Link_Embed { get; set; } = string.Empty;
        public string Link_M3U8 { get; set; } = string.Empty;
    }

    public class Category
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }

    public class Country
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }

    // Class để deserialize JSON response
    public class ApiResponse
    {
        public bool Status { get; set; }
        public required List<Movie> Items { get; set; } = new();
        public required string PathImage { get; set; } = string.Empty;
    }

    // ViewModel cho đăng ký
    public class RegisterViewModel
    {
        [Required]
        public string TaiKhoan { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không khớp.")]
        public string ConfirmMatKhau { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }



}


