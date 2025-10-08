using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using NewMovies.Models;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NewMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly DBContext _context;


        private readonly string _jsonFilePath = "wwwroot/data/movies.json"; // File JSON lưu phim

        public HomeController(HttpClient httpClient, DBContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Đường dẫn API
                string apiUrl = "https://ophim1.com/danh-sach/phim-moi-cap-nhat?page=9";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON thành object
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    if (apiResponse?.Items != null)
                    {
                        List<Movie> movies = apiResponse.Items.Take(20).ToList();
                        foreach (var movie in movies)
                        {
                            if (!string.IsNullOrEmpty(movie.Thumb_Url))
                            {
                                // Thêm pathImage vào đường dẫn ảnh
                                movie.Thumb_Url = $"{apiResponse.PathImage}{movie.Thumb_Url}";
                            }
                            if (!string.IsNullOrEmpty(movie.Poster_Url))
                            {
                                // Thêm pathImage vào đường dẫn ảnh
                                movie.Poster_Url = $"{apiResponse.PathImage}{movie.Poster_Url}";
                            }
                        }
                        return View(movies);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                        return View(new List<Movie>());
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể kết nối đến API.";
                    return View(new List<Movie>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(new List<Movie>());
            }
        }

        [Route("/chi-tiet/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            try
            {
                // Đường dẫn API
                string apiUrl = $"https://ophim1.com/phim/{slug}";

                // Gọi API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ response
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON thành object
                    var movieDetailResponse = JsonConvert.DeserializeObject<MovieDetailResponse>(jsonResponse);
                    if (movieDetailResponse?.Movie != null)
                    {
                        var movieDetail = movieDetailResponse.Movie;

                        // Truyền chi tiết phim vào View
                        return View(movieDetail);
                    }
                    else
                    {
                        // Nếu gọi API thất bại, trả về thông báo lỗi
                        ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                        return View(null);
                    }
                }
                else
                {
                    // Nếu response không thành công, trả về thông báo lỗi
                    ViewBag.ErrorMessage = "Không thể kết nối đến API.";
                    return View(null);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(null);
            }
        }

        [Route("/xem-phim/{slug}")]
        public async Task<IActionResult> Watch(string slug)
        {
            try
            {
                // Đường dẫn API
                string apiUrl = $"https://ophim1.com/phim/{slug}";

                // Gọi API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ response
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON thành object
                    var movieDetailResponse = JsonConvert.DeserializeObject<MovieDetailResponse>(jsonResponse);
                    if (movieDetailResponse?.Movie != null)
                    {
                        var movieDetail = movieDetailResponse.Movie;
                        movieDetail.Episodes = movieDetailResponse.Episodes;

                        // Lấy tập đầu tiên
                        var firstEpisode = movieDetail.Episodes?.FirstOrDefault()?.Server_Data?.FirstOrDefault();
                        if (firstEpisode != null)
                        {
                            ViewBag.FirstEpisodeLink = firstEpisode.Link_Embed;
                        }

                        // Truyền chi tiết phim vào View
                        return View(movieDetail);
                    }
                    else
                    {
                        // Nếu gọi API thất bại, trả về thông báo lỗi
                        ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                        return View(null);
                    }
                }
                else
                {
                    // Nếu response không thành công, trả về thông báo lỗi
                    ViewBag.ErrorMessage = "Không thể kết nối đến API.";
                    return View(null);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(null);
            }
        }

        [Route("/hoi-dap")]
        public IActionResult FAQs()
        {
            return View();
        }

        [Route("/phim-moi")]
        public async Task<IActionResult> New()
        {
            try
            {
                // Đường dẫn API
                string apiUrl = "https://ophim1.com/danh-sach/phim-moi-cap-nhat?page=2";

                // Gọi API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ response
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON thành object
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    if (apiResponse?.Items != null)
                    {
                        List<Movie> movies = apiResponse.Items;

                        // Giới hạn số lượng phim tải lên là 20
                        movies = movies.Take(20).ToList();

                        // Ghép pathImage với thumb_url và poster_url
                        foreach (var movie in movies)
                        {
                            if (!string.IsNullOrEmpty(movie.Thumb_Url))
                            {
                                movie.Thumb_Url = $"{apiResponse.PathImage}{movie.Thumb_Url}";
                            }
                            if (!string.IsNullOrEmpty(movie.Poster_Url))
                            {
                                movie.Poster_Url = $"{apiResponse.PathImage}{movie.Poster_Url}";
                            }
                        }

                        // Truyền danh sách phim vào View
                        return View(movies);
                    }
                    else
                    {
                        // Nếu gọi API thất bại, trả về danh sách rỗng và hiển thị thông báo
                        ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                        return View(new List<Movie>());
                    }
                }
                else
                {
                    // Nếu response không thành công, trả về danh sách rỗng và hiển thị thông báo
                    ViewBag.ErrorMessage = "Không thể kết nối đến API.";
                    return View(new List<Movie>());
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(new List<Movie>());
            }
        }

        [Route("/phim-bo")]
        public async Task<IActionResult> Series()
        {
            try
            {
                List<Movie> movies = new List<Movie>();
                int numberOfPages = 5; // Số lượng trang muốn lấy dữ liệu

                // Kiểm tra nếu file JSON đã tồn tại, đọc từ file thay vì gọi API
                if (System.IO.File.Exists(_jsonFilePath))
                {
                    string fileContent = await System.IO.File.ReadAllTextAsync(_jsonFilePath);

                    // Deserialize JSON thành object
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(fileContent);
                    if (apiResponse?.Items != null) movies = apiResponse.Items;
                }
                // Nếu file JSON chưa tồn tại, gọi API từ nhiều trang
                else
                {
                    for (int page = 1; page <= numberOfPages; page++)
                    {
                        // Gọi API để lấy danh sách phim mới cập nhật từ từng trang
                        string apiUrl = $"https://ophim1.com/danh-sach/phim-moi-cap-nhat?page={page}";
                        string jsonResponse = await _httpClient.GetStringAsync(apiUrl);

                        // Phân tích cú pháp JSON từ API
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                        if (apiResponse?.Items != null)
                        {
                            movies.AddRange(apiResponse.Items); // Thêm các phim từ trang hiện tại vào danh sách
                        }
                    }

                    // Lưu dữ liệu vào file JSON để tái sử dụng sau này
                    var initialApiResponse = new ApiResponse { Status = true, Items = movies, PathImage = "/images/" };

                    // Serialize object thành JSON
                    string initialJson = JsonConvert.SerializeObject(initialApiResponse, Formatting.Indented);
                    await System.IO.File.WriteAllTextAsync(_jsonFilePath, initialJson);
                }

                // Duyệt qua danh sách phim để cập nhật đường dẫn ảnh và type
                foreach (var movie in movies)
                {
                    // Kiểm tra nếu đường dẫn ảnh chưa bắt đầu bằng https thì thêm tiền tố
                    if (!string.IsNullOrEmpty(movie.Thumb_Url) && !movie.Thumb_Url.StartsWith("http"))
                    {
                        movie.Thumb_Url = $"https://img.ophim.live/uploads/movies/{movie.Thumb_Url}";
                    }

                    if (!string.IsNullOrEmpty(movie.Poster_Url) && !movie.Poster_Url.StartsWith("http"))
                    {
                        movie.Poster_Url = $"https://img.ophim.live/uploads/movies/{movie.Poster_Url}";
                    }

                    // Nếu chưa có Type, gọi API chi tiết để cập nhật thông tin phim
                    if (string.IsNullOrEmpty(movie.Type))
                    {
                        string detailApiUrl = $"https://ophim1.com/phim/{movie.Slug}";
                        HttpResponseMessage detailResponse = await _httpClient.GetAsync(detailApiUrl);
                        if (detailResponse.IsSuccessStatusCode)
                        {
                            string detailJsonResponse = await detailResponse.Content.ReadAsStringAsync();
                            var movieDetailResponse = JsonConvert.DeserializeObject<MovieDetailResponse>(detailJsonResponse);
                            if (movieDetailResponse?.Movie != null)
                            {
                                movie.Type = movieDetailResponse.Movie.Type;
                            }
                        }
                    }
                }

                // Cập nhật lại file JSON sau khi bổ sung thông tin
                var updatedApiResponse = new ApiResponse { Status = true, Items = movies, PathImage = "/images/" };
                string updatedJson = JsonConvert.SerializeObject(updatedApiResponse, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(_jsonFilePath, updatedJson);

                // Lọc ra danh sách phim bộ (type = series) và giới hạn số lượng là 20
                var seriesMovies = movies.Where(m => m.Type == "series").Take(20).ToList();

                return View(seriesMovies);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo lỗi
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(new List<Movie>());
            }
        }





        [Route("/phim-le")]
        public async Task<IActionResult> Single()
        {
            try
            {
                List<Movie> movies = new List<Movie>();
                int numberOfPages = 5; // Số lượng trang muốn lấy dữ liệu

                // Kiểm tra nếu file JSON đã tồn tại, đọc dữ liệu cũ
                if (System.IO.File.Exists(_jsonFilePath))
                {
                    string fileContent = await System.IO.File.ReadAllTextAsync(_jsonFilePath);

                    // Deserialize JSON thành object
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(fileContent);
                    if (apiResponse?.Items != null) movies = apiResponse.Items.ToList(); // Giữ dữ liệu cũ
                }

                // Gọi API từ nhiều trang và bổ sung dữ liệu mới
                for (int page = 1; page <= numberOfPages; page++)
                {
                    string apiUrl = $"https://ophim1.com/danh-sach/phim-moi-cap-nhat?page={page}";
                    string jsonResponse = await _httpClient.GetStringAsync(apiUrl);
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    // Thêm phim mới từ API, tránh trùng lặp
                    if (apiResponse?.Items != null)
                    {
                        foreach (var newMovie in apiResponse.Items)
                        {
                            if (!movies.Any(m => m.Slug == newMovie.Slug)) 
                            {
                                movies.Add(newMovie);
                            }
                        }
                    }
                }

                // Duyệt danh sách phim để sửa đường dẫn ảnh và cập nhật type
                foreach (var movie in movies)
                {
                    if (!string.IsNullOrEmpty(movie.Thumb_Url) && !movie.Thumb_Url.StartsWith("http"))
                    {
                        movie.Thumb_Url = $"https://img.ophim.live/uploads/movies/{movie.Thumb_Url}";
                    }

                    if (!string.IsNullOrEmpty(movie.Poster_Url) && !movie.Poster_Url.StartsWith("http"))
                    {
                        movie.Poster_Url = $"https://img.ophim.live/uploads/movies/{movie.Poster_Url}";
                    }

                    if (string.IsNullOrEmpty(movie.Type))
                    {
                        string detailApiUrl = $"https://ophim1.com/phim/{movie.Slug}";
                        HttpResponseMessage detailResponse = await _httpClient.GetAsync(detailApiUrl);
                        if (detailResponse.IsSuccessStatusCode)
                        {
                            string detailJsonResponse = await detailResponse.Content.ReadAsStringAsync();
                            var movieDetailResponse = JsonConvert.DeserializeObject<MovieDetailResponse>(detailJsonResponse);
                            if (movieDetailResponse?.Movie != null)
                            {
                                movie.Type = movieDetailResponse.Movie.Type;
                            }
                        }
                    }
                }

                // Cập nhật lại file JSON với danh sách phim đã chỉnh sửa
                var updatedApiResponse = new ApiResponse { Status = true, Items = movies, PathImage = "/images/" };

                // Serialize object thành JSON
                string updatedJson = JsonConvert.SerializeObject(updatedApiResponse, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(_jsonFilePath, updatedJson);

                // Lọc danh sách phim lẻ (type = single), giới hạn 20 phim
                var singleMovies = movies.Where(m => m.Type == "single").Take(20).ToList();

                return View(singleMovies);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View(new List<Movie>());
            }
        }


        [HttpGet]
        [Route("/dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/dang-nhap")]
        public async Task<IActionResult> Login(QuanLyTaiKhoan user)
        {
            var existingUser = await _context.QuanLyTaiKhoans
                .FirstOrDefaultAsync(u => u.TaiKhoan == user.TaiKhoan && u.MatKhau == user.MatKhau);

            if (existingUser != null)
            {
                // Đăng nhập thành công
                HttpContext.Session.SetString("UserId", existingUser.Id.ToString()); // Lưu ID người dùng vào session
                HttpContext.Session.SetString("Username", existingUser.TaiKhoan); // Lưu tên đăng nhập vào session

                return RedirectToAction("Index"); //Nếu dùng toRoute thì dùng dang-nhap
            }

            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View(user);
        }



        [HttpGet]
        [Route("/dang-ky")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/dang-ky")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new QuanLyTaiKhoan
                {
                    TaiKhoan = model.TaiKhoan,
                    MatKhau = model.MatKhau,
                    Email = model.Email,
                    VaiTro = "User",
                    NgayDangKy = DateTime.Now
                };

                _context.QuanLyTaiKhoans.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View(model);
        }


        [Route("/dang-xuat")]
        public IActionResult Logout()
        {
            // Xóa thông tin người dùng khỏi session
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Username");

            return RedirectToAction("Index");
        }



        [Route("/Search")]
        public async Task<IActionResult> Search(string Keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(Keyword))
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập từ khóa tìm kiếm.";
                    return View("Search", new List<Movie>());
                }

                List<Movie> allMovies = new List<Movie>();
                int currentPage = 1;
                int maxPages = 8; // Giới hạn số trang để tránh gọi API quá nhiều

                // Lặp qua các trang để lấy dữ liệu
                while (currentPage <= maxPages)
                {
                    string apiUrl = $"https://ophim1.com/danh-sach/phim-moi-cap-nhat?page={currentPage}";
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.ErrorMessage = "Không thể kết nối đến API.";
                        return View("Search", new List<Movie>());
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    if (apiResponse?.Items == null || !apiResponse.Items.Any())
                    {
                        break; // Dừng nếu không còn dữ liệu
                    }

                    // Thêm phim từ trang hiện tại vào danh sách tổng
                    allMovies.AddRange(apiResponse.Items);

                    // Cập nhật URL ảnh
                    foreach (var movie in apiResponse.Items)
                    {
                        if (!string.IsNullOrEmpty(movie.Thumb_Url))
                        {
                            movie.Thumb_Url = $"{apiResponse.PathImage}{movie.Thumb_Url}";
                        }
                        if (!string.IsNullOrEmpty(movie.Poster_Url))
                        {
                            movie.Poster_Url = $"{apiResponse.PathImage}{movie.Poster_Url}";
                        }
                    }

                    currentPage++;
                }

                // Lọc phim theo từ khóa
                var filteredMovies = allMovies
                    .Where(m => (m.Name != null && m.Name.ToLower().Contains(Keyword.ToLower())) ||
                                (m.Origin_Name != null && m.Origin_Name.ToLower().Contains(Keyword.ToLower())))
                    .ToList();

                if (!filteredMovies.Any())
                {
                    ViewBag.ErrorMessage = "Không tìm thấy kết quả nào.";
                    return View("Search", new List<Movie>());
                }

                return View("Search", filteredMovies);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Đã có lỗi xảy ra: {ex.Message}";
                return View("Search", new List<Movie>());
            }
        }



    }
}
