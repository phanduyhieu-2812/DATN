using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class PieChartViewComponent : ViewComponent
    {
        private readonly WebDatnContext db;

        public PieChartViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {

            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;
            int currentDay = currentDate.Day;


            var total = db.Orders
                .Where(p => p.CreateAt.Month == currentMonth && p.CreateAt.Year == currentYear && p.CreateAt.Day == currentDay)
                .Sum(p => p.Total);
            var result = new DasboradVM { DaylyEarn = total };
            return View("PieChart", result);

            
        }
    }
}
