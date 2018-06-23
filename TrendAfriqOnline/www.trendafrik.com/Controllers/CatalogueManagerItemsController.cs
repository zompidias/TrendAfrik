using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TABusinessLayer;
using TrendAfriqOnline.Models;
using PagedList;
using PagedList.Mvc;

namespace TrendAfriqOnline.Controllers
{
    public class CatalogueManagerItemsController : Controller
    {
        TrendAfriqEntities db = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();

        public ActionResult Index()
        {
           var ct = db.SellerItemss.Include("Catalogue").Include("SellerDetails").ToList();
            return View(ct);
        }

        
        //
        // GET: /CatalogueManagerItems/Create

        public ActionResult Create()
        {
            ViewBag.SellerId = new SelectList(dbset.SellerDetailss.OrderBy(x => x.sellerName), "sellerId", "sellerName");
            ViewBag.CatalogueId = new SelectList(dbset.Catalogues.OrderBy(x => x.catalogueName), "catalogueId", "catalogueName");
           
            return View();
        }

        //
        // POST: /CatalogueManagerItems/Create

        [HttpPost]
        public ActionResult Create(TABusinessLayer.SellerItems collection)
        {
            if (ModelState.IsValid)
            {
                var picc = collection.pictureC;
                dbset.AddItemsToDB(collection);
                return RedirectToAction("Index");
            }

            ViewBag.SellerId = new SelectList(dbset.SellerDetailss.OrderBy(x => x.sellerName), "sellerId", "sellerName");
            ViewBag.CatalogueId = new SelectList(dbset.Catalogues.OrderBy(x => x.catalogueName), "catalogueId", "catalogueName");
           
            return View();  
            
        }

        //
        // GET: /CatalogueManagerItems/Edit/5

        public ActionResult Edit(int id)
        {
            TABusinessLayer.SellerItems ctItems = dbset.SellerItemss.Single(emp => emp.itemId == id);
            if (ctItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerId = new SelectList(dbset.SellerDetailss.OrderBy(x => x.sellerName), "sellerId", "sellerName", ctItems.sellerId);
            ViewBag.CatalogueId = new SelectList(dbset.Catalogues.OrderBy(x => x.catalogueName), "catalogueId", "catalogueName", ctItems.catalogueId);
           
            return View(ctItems);
        }

        //
        // POST: /CatalogueManagerItems/Edit/5

        [HttpPost]
        public ActionResult Edit(TABusinessLayer.SellerItems collection)
        {
            if (ModelState.IsValid)
            {
                dbset.SaveChangesSellerItemsToDB(collection);
                //db.Entry(fooditem).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerId = new SelectList(dbset.SellerDetailss.OrderBy(x => x.sellerName), "sellerId", "sellerName", collection.sellerId);
            ViewBag.CatalogueId = new SelectList(dbset.Catalogues.OrderBy(x => x.catalogueName), "catalogueId", "catalogueName", collection.catalogueId);
           
            return View(collection);
        }

        //
        // GET: /CatalogueManagerItems/Delete/5

        [HttpPost]
        public ActionResult Delete(decimal id)
        {
            dbset.DeleteCatalogueFromDB(id);
            return RedirectToAction("Index");

            //return View();
        }
    }
}
