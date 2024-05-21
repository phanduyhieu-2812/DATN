using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class NewFeedBackViewComponent: ViewComponent
    {
        private readonly WebDatnContext db;

        public NewFeedBackViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {

            var count = db.Feedbacks
            .Where(p => p.Status == "Chưa xem")
            .Count();

            var result = new DasboradVM { NewFeedback = count };
            return View("NewFeedback", result);
        }
    }
}
