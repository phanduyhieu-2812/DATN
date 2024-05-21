using Microsoft.AspNetCore.Mvc;
using webbanhang.Helpers;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var count = HttpContext.Session.Get<List<CartItem>>(MySetting.CARTKEY) ?? new List<CartItem>();
            return View("CartPanel",new CartModel
            {
                quantity = count.Sum(p => p.SoLuong),
                total = count.Sum(p => p.ThanhTien)
            }
            );
        }
    }
}
