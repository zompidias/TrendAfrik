using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TABusinessLayer;
using TrendAfriqOnline.Models;

namespace TrendAfriqOnline.Controllers
{
    public class CatalogueManagerSellersController : Controller
    {
        TrendAfriqEntities db = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();


        public ActionResult Index()
        {
            var ct = db.SellerDetailss;

            return View(ct.ToList());
        }

        

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CatalogueMasterSeller/Create

        [HttpPost]
        public ActionResult Create(TABusinessLayer.SellerDetails collection)
        {
            if (ModelState.IsValid)
            {
                dbset.AddSellerDetailsToDB(collection);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /CatalogueMasterSeller/Edit/5

        public ActionResult Edit(int id)
        {
            TABusinessLayer.SellerDetails ctitem = dbset.SellerDetailss.Single(emp => emp.sellerId == id);
            if (ctitem == null)
            {
                return HttpNotFound();
            }

            return View(ctitem);
        }

        //
        // POST: /CatalogueMasterSeller/Edit/5

        [HttpPost]
        public ActionResult Edit(TABusinessLayer.SellerDetails collection)
        {
            if (ModelState.IsValid)
            {
                var num = collection.sellerId;
                dbset.SaveChangesSellerDetailsToDB(collection);
                return RedirectToAction("Index");
            }

            return View(collection);
            
        }

        
    }
}
