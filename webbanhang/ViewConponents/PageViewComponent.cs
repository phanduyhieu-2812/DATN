using Microsoft.AspNetCore.Mvc;
using webbanhang.ViewModels;
using webbanhang.Data;

namespace webbanhang.ViewConponents
{
    public class PageViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public PageViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var totalProducts = db.Products
            .Count(); // Đếm số lượng sản phẩm

            var totalPages = Math.Ceiling((double)totalProducts / 9 ); // Tính toán số trang cần thiết

            return View("Page", totalPages);

            
        }

    }
}
