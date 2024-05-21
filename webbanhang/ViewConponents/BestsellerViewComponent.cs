using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class BestsellerViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public BestsellerViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Products
            .OrderBy(p => p.Byturn)
            .Take(6)
            .Select(p => new HangHoaVM
            {
                MaHh = p.ProductId,
                TenHh = p.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Image

            });
            return View("Bestseller", data);
        }

    }
}
