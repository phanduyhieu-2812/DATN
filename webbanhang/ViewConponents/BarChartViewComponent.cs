using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class BarChartViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public BarChartViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {           
            var THEWAY = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 3).Sum(p => p.Price);
            var Badhabit = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 9).Sum(p => p.Price);

            var Davies = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 6).Sum(p => p.Price);

            var Dcoin = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 8).Sum(p => p.Price);

            var Uncover = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 5).Sum(p => p.Price);
            var Zun = db.OderDetails.Include(p => p.Product).Where(p => p.Product.BrandId == 7).Sum(p => p.Price);
            var result = new BarChart { Theway = THEWAY,Badhabit = Badhabit, Davies = Davies, Dcoin = Dcoin, Uncover = Uncover, Zun = Zun };

            return View("BarChart",result);
        }
    } 
}
