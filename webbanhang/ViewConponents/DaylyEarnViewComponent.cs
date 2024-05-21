using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class DaylyEarnViewComponent: ViewComponent
    {
        private readonly WebDatnContext db;

        public DaylyEarnViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {

            var Tshirt = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 1 ).Sum(p => p.Price);
            var JACKET = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 2).Sum(p => p.Price);
            var CROPTOP = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 3).Sum(p => p.Price);
            var HOODIE = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 4).Sum(p => p.Price);
            var BAG = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 5).Sum(p => p.Price);
            var WALLET = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 6).Sum(p => p.Price);
            var CAP = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 7).Sum(p => p.Price);
            var PANT = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 8).Sum(p => p.Price);
            var SHORT = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 9).Sum(p => p.Price);
            var JEAN = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 10).Sum(p => p.Price);
            var BLAZER = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 11).Sum(p => p.Price);
            var BOMBER = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 12).Sum(p => p.Price);
            var HAT = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 13).Sum(p => p.Price);
            var CARAVAT = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 14).Sum(p => p.Price);
            var SHIRT = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 15).Sum(p => p.Price);
            var Cardigan = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 16).Sum(p => p.Price);
            var Slides = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 17).Sum(p => p.Price);
            var BACKPACK = db.OderDetails.Include(p => p.Product).Where(p => p.Product.GenreId == 18).Sum(p => p.Price);


            var total = BACKPACK+ Tshirt + JACKET + CROPTOP + HOODIE + BAG + WALLET + SHIRT + CARAVAT + CAP + PANT + SHORT + JEAN + BLAZER + BOMBER + HAT + Cardigan + Slides;


            var Tshirti = (Tshirt / total) * 100;
            var JACKETi = (JACKET / total) * 100;
            var CROPTOPi = (CROPTOP / total) * 100;
            var HOODIEi = (HOODIE / total) * 100;
            var BAGi = (BAG / total) * 100;
            var BACKPACKi = (BACKPACK / total) * 100;

            var WALLETi = (WALLET / total) * 100;

            var SHIRTi = (SHIRT / total) * 100;

            var CARAVATi = (CARAVAT / total) * 100;

            var CAPi = (CAP / total) * 100;

            var PANTi = (PANT / total) * 100;

            var SHORTi = (SHORT / total) * 100;

            var JEANi = (JEAN / total) * 100;

            var BOMBERi = (BOMBER / total) * 100;

            var BLAZERi = (BLAZER / total) * 100;

            var HATi = (HAT / total) * 100;

            var Cardigani = (Cardigan / total) * 100;

            var Slidesi = (Slides / total) * 100;

            var result = new PieChartVM
            {
                Tshirt = Tshirti,
                JACKET = JACKETi,
                CROPTOP = CROPTOPi,
                HOODIE = HOODIEi,
                BACKPACK = BACKPACKi,
                BAG = BAGi,
                WALLET = WALLETi,
                CAP = CAPi,
                PANT = PANTi,
                SHORT = SHORTi,
                JEAN = JEANi,
                BOMBER = BOMBERi,
                BLAZER = BLAZERi,
                HAT = HATi,
                CARAVAT = CARAVATi,
                SHIRT = SHIRTi,
                Cardigan = Cardigani,
                Slides = Slidesi,
            };

               

            return View("DaylyEarn", result);
        }
    }
}
