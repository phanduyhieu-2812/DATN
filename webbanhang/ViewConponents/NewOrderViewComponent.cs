using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class NewOrderViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public NewOrderViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            

                var count = db.Orders
                .Where(p => p.Status == "Thanh toán thành công" || p.Status == "Đã thanh toán")
                .Count();

            var result = new DasboradVM { NewOrder = count };
            return View("NewOrder", result);
        }
    }
}
