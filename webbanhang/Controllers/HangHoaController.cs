using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Text;
using webbanhang.Data;
using webbanhang.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using X.PagedList;

namespace webbanhang.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly WebDatnContext db;

        public HangHoaController(WebDatnContext context)
        {
            db = context;
        }



        public IActionResult Index( int? page , int? order, int? loai, int? Brand_ID, int? Price)
        {

            int pageNumber = (page ?? 1);
            var HangHoas = db.Products.AsQueryable();
            if (loai.HasValue && Brand_ID.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.GenreId == loai && p.BrandId == Brand_ID);
            }
            else if (loai.HasValue && Price.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.GenreId == loai && p.Price <= Price);
            }
            else if (Brand_ID.HasValue && Price.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.BrandId == Brand_ID && p.Price <= Price);
            }
            else if (Brand_ID.HasValue && Price.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.BrandId == Brand_ID && p.Price <= Price && p.GenreId == loai);
                 
            }
            else if (loai.HasValue || Brand_ID.HasValue || Price.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.GenreId == loai || p.BrandId == Brand_ID || p.Price <= Price);
            }
            if(order.HasValue)
            {
                if (order.Value == 1) { HangHoas = HangHoas.OrderBy(p => p.ProductName); }
                else if (order.Value == 2) { HangHoas = HangHoas.OrderByDescending(p => p.ProductName); }
                else if (order.Value == 3) { HangHoas = HangHoas.OrderByDescending(p => p.Price); }
                else if (order.Value == 4) { HangHoas = HangHoas.OrderBy(p => p.Price); }
            }

            var data = db.Genres.Select(lo => new HangHoaVM
            {
                MaLoai = lo.GenreId,
                Tenloai = lo.GenreName,
                Icon = lo.Icon
            });

            var data1 = db.Products.Where(lo => lo.BrandId <= 8).Select(lo => new HangHoaVM
            {
                Mabrand = lo.BrandId,
                Tenbrand = lo.Brand.BrandName
            }).Distinct();
            var reusult = HangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image ?? "",
                Tenloai = p.Genre.GenreName,
                Motangan = p.Specifications ?? "",
                Brand = p.Brand.BrandName ?? ""
            }).ToPagedList(pageNumber, 9);
            ViewBag.Data = data;
            ViewBag.Data1 = data1;
            ViewBag.Price = Price;
            ViewBag.order = order; 
            ViewBag.loai = loai;
            ViewBag.brand = Brand_ID;
            return View(reusult);
        }


        public IActionResult LoaiIndex(int? Loai, int? page1, int? order)
        {
            int pageNumber = (page1 ?? 1);
            var HangHoas = db.Products.Where(p => p.GenreId == Loai).AsQueryable();
            if (order.HasValue)
            {
                if (order.Value == 1) { HangHoas = HangHoas.OrderBy(p => p.ProductName); }
                else if (order.Value == 2) { HangHoas = HangHoas.OrderByDescending(p => p.ProductName); }
                else if (order.Value == 3) { HangHoas = HangHoas.OrderByDescending(p => p.Price); }
                else if (order.Value == 4) { HangHoas = HangHoas.OrderBy(p => p.Price); }
            }
            var reusult = HangHoas.Where(p => p.GenreId == Loai).Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image ?? "",
                Tenloai = p.Genre.GenreName,
                Motangan = p.Specifications ?? "",
                Brand = p.Brand.BrandName ?? ""
            }).ToPagedList(pageNumber, 9);
            ViewBag.Loai = Loai;
            ViewBag.order = order;  
            return View(reusult); 
        }

        public IActionResult BrandIndex(int? Brand, int? page1, int? order)
        {
            int pageNumber = (page1 ?? 1);
            var HangHoas = db.Products.Where(p => p.BrandId == Brand).AsQueryable();
            if (order.HasValue)
            {
                if (order.Value == 1) { HangHoas = HangHoas.OrderBy(p => p.ProductName); }
                else if (order.Value == 2) { HangHoas = HangHoas.OrderByDescending(p => p.ProductName); }
                else if (order.Value == 3) { HangHoas = HangHoas.OrderByDescending(p => p.Price); }
                else if (order.Value == 4) { HangHoas = HangHoas.OrderBy(p => p.Price); }
            }
            var reusult = HangHoas.Where(p => p.BrandId == Brand).Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image ?? "",
                Tenloai = p.Genre.GenreName,
                Motangan = p.Specifications ?? "",
                Brand = p.Brand.BrandName ?? ""
            }).ToPagedList(pageNumber, 9);
            ViewBag.Loai = Brand;
            ViewBag.order = order;
            return View(reusult);
        }




        public IActionResult Search(string? query)
        {

            var HangHoas = db.Products.AsQueryable();

            if (query != null)
            {
                HangHoas = HangHoas.Where(p => p.ProductName.Contains(query) || p.Genre.GenreName.Contains(query) || p.Brand.BrandName.Contains(query));
            }

            var reusult = HangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image ?? "",
                Tenloai = p.Genre.GenreName,
                Motangan = p.Specifications ?? "",
                Brand = p.Brand.BrandName ?? ""
            });


            return View(reusult);

        }


        [HttpGet]
        public IActionResult ChiTietHH(int id)
        {
            var data = db.Products.Include(p => p.Genre).SingleOrDefault(p => p.ProductId == id);
            var dataa = db.Feedbacks.Where(p => p.ProductId == id);
            double averageRate = dataa.Average(p => p.RateStar);
            if (dataa == null)
            {
                averageRate = 3;
            }
            int roundedAverage = (int)Math.Round(averageRate);

            // Làm tròn giá trị trung bình và gán vào biến roundedAverage

            if (data == null)
            {
                TempData["Message"] = "KHông tìm thấy thông tin sản phẩm";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                MaHh = data.ProductId,
                TenHh = data.ProductName,
                DonGia = data.Price ?? 0,
                ChiTiet = data.Description,
                Hinh = data.Image,
                Motangan = data.Specifications,
                Tenloai = data.Genre.GenreName,                
                DiemDanhGia = 5,
                ratestar = roundedAverage              
            };
            var data1 = db.Feedbacks.Where(p => p.ProductId == id).Select(p =>  new ChiTietHangHoaVM 
            {

            }
                );

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Feedback(int id, FeedbackVM model) 
        {
            if (ModelState.IsValid) 
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                // Tạo đối tượng Random
                Random random = new Random();

                // Khởi tạo một chuỗi rỗng để lưu trữ chuỗi ngẫu nhiên
                string result = "";

                // Tạo một chuỗi ngẫu nhiên bằng cách chọn ngẫu nhiên ký tự từ chuỗi chars và nối vào chuỗi kết quả
                for (int i = 0; i < 8; i++)
                {
                    result += chars[random.Next(chars.Length)];
                }
                var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name").Value;
                var khachhang = db.Products.SingleOrDefault(p => p.ProductId == id);
                var feedback = new Feedback
                {
                    FeedbackId = result,
                    ProductId = id,
                    GenreId = khachhang.GenreId,
                    RateStar = model.rate,
                    CreateAt = DateTime.Now,
                    CreateBy = userName,
                    Content = model.Content,
                    Status = "Chưa xem"

                };
                db.Database.BeginTransaction();
                db.Database.CommitTransaction();
                db.Add(feedback);
                db.SaveChanges();
                
            }
            return View();
        }
    }
}




