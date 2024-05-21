using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webbanhang.Data;
using webbanhang.Models;
using webbanhang.ViewModels;

namespace webbanhang.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly WebDatnContext db;

        public HomeController(WebDatnContext context)
        {
            db = context;
        }


        public IActionResult Index(int? Loai)
        {
            var HangHoas = db.Products.AsQueryable();

            if (Loai.HasValue)
            {
                HangHoas = HangHoas.Where(p => p.GenreId == Loai.Value && p.Byturn >= 100);
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

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Secure()
        {
            return View();
        }
        
        
        
    }
}
