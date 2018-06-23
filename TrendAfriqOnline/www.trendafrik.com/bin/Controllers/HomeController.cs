using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TABusinessLayer;
using TrendAfriqOnline.Models;

namespace TrendAfriqOnline.Controllers
{
    public class HomeController : Controller
    {
        TrendAfriqEntities ta = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();

        public ActionResult Index()
        {
            var topFoods = GetLatestEntries(6);
            return View(topFoods); 
            
        }

        private List<TrendAfriqOnline.Models.SellerItems> GetLatestEntries(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return ta.SellerItemss
                .OrderByDescending(a => a.itemId)
                .Take(count)
                .ToList();
        }

    }
}
