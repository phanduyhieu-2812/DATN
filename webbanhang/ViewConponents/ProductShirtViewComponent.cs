using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class ProductShirtViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public ProductShirtViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Products.Where(p => p.Views >= 300 && p.GenreId == 16).Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image ?? "",
                Tenloai = p.Genre.GenreName,
                Motangan = p.Specifications ?? "",
                Brand = p.Brand.BrandName ?? ""
            });
            return View("ProductShirt", data);
        }
    }
}
