using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public MenuLoaiViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Genres.Select(lo => new MenuLoai
            {
                MaLoai = lo.GenreId,
                TenLoai = lo.GenreName,
                SoLuong = lo.Products.Count,
                icon = lo.Icon
            });
            return View(data);
        }
    }
    
}
