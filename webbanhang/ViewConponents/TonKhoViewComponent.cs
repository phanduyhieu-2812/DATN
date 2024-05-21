using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;
using webbanhang.ViewModels;

namespace webbanhang.ViewConponents
{
    public class TonKhoViewComponent: ViewComponent
    {
        private readonly WebDatnContext db;

        public TonKhoViewComponent(WebDatnContext context) => db = context;

        public IViewComponentResult Invoke()
        {

            var Tshirt = db.Products.Where(p => p.GenreId == 1).Sum(p => p.Quantity);
            var JACKET = db.Products.Where(p => p.GenreId == 2).Sum(p => p.Quantity);
            var CROPTOP = db.Products.Where(p => p.GenreId == 3).Sum(p => p.Quantity);
            var HOODIE = db.Products.Where(p => p.GenreId == 4).Sum(p => p.Quantity);
            var BAG = db.Products.Where(p => p.GenreId == 5).Sum(p => p.Quantity);
            var WALLET = db.Products.Where(p => p.GenreId == 6).Sum(p => p.Quantity);
            var CAP = db.Products.Where(p => p.GenreId == 7).Sum(p => p.Quantity);
            var PANT = db.Products.Where(p => p.GenreId == 8).Sum(p => p.Quantity);
            var SHORT = db.Products.Where(p => p.GenreId == 9).Sum(p => p.Quantity);
            var JEAN = db.Products.Where(p => p.GenreId == 10).Sum(p => p.Quantity);
            var BLAZER = db.Products.Where(p => p.GenreId == 11).Sum(p => p.Quantity);
            var BOMBER = db.Products.Where(p => p.GenreId == 12).Sum(p => p.Quantity);
            var HAT = db.Products.Where(p => p.GenreId == 13).Sum(p => p.Quantity);
            var CARAVAT = db.Products.Where(p => p.GenreId == 14).Sum(p => p.Quantity); ;
            var SHIRT = db.Products.Where(p => p.GenreId == 15).Sum(p => p.Quantity);
            var Cardigan = db.Products.Where(p => p.GenreId == 16).Sum(p => p.Quantity);
            var Slides = db.Products.Where(p => p.GenreId == 17).Sum(p => p.Quantity);
            var BACKPACK = db.Products.Where(p => p.GenreId == 18).Sum(p => p.Quantity);


            var total = BACKPACK + Tshirt + JACKET + CROPTOP + HOODIE + BAG + WALLET + SHIRT + CARAVAT + CAP + PANT + SHORT + JEAN + BLAZER + BOMBER + HAT + Cardigan + Slides;


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



            return View("TonKho",result);
        }
    }
}
