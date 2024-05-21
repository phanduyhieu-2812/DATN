using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class MenuLoaiIndexViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public MenuLoaiIndexViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Products.Where(lo => lo.BrandId <= 8).Select(lo => new HangHoaVM
            {
                Mabrand = lo.BrandId,
                Tenbrand = lo.Brand.BrandName
            }).Distinct();
            return View("MenuLoaiIndex",data);
        }
    }
}
