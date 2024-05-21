using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;

namespace webbanhang.ViewConponents
{
    public class PageCustomerViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public PageCustomerViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var totalProducts = db.Accounts
            .Count(); // Đếm số lượng sản phẩm

            var totalPages = Math.Ceiling((double)totalProducts / 5); // Tính toán số trang cần thiết

            return View("PageCustomer", totalPages);


        }
    }
}
