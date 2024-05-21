using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class MonthlyEarnViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public MonthlyEarnViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            var total = db.Orders
                .Where(p => p.CreateAt.Month == currentMonth && p.CreateAt.Year == currentYear)
                .Sum(p => p.Total);
            var result = new DasboradVM { MonthlyEarn = total };
            return View("MonthlyEarn",result);
        }

    }
}

