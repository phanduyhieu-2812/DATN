using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class AddBrandViewComponent: ViewComponent
    {
        private readonly WebDatnContext db;

        public AddBrandViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Brands.Select(lo => new HangHoaVM
            {
                Mabrand = lo.BrandId,
                Tenbrand = lo.BrandName
            });
            var data1 = db.Genres.Select(lo => new HangHoaVM
            {
                MaLoai = lo.GenreId,
                Tenloai = lo.GenreName
            });
            ViewBag.Brands = data;
            ViewBag.Genres = data1;
            return View("AddBrand");
        }
    }
}
