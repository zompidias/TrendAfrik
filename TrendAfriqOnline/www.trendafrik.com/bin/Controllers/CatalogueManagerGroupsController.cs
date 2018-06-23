using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrendAfriqOnline.Models;
using TABusinessLayer;


namespace TrendAfriqOnline.Controllers
{
    public class CatalogueManagerGroupsController : Controller
    {
        TrendAfriqEntities db = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();

        public ActionResult Index()
        {
            var ct = db.Catalogues; 

            return View(ct.ToList());
        }

        //
        // GET: /CatalogueMasterGroups/Details/5

        

        //
        // GET: /CatalogueMasterGroups/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CatalogueMasterGroups/Create

        [HttpPost]
        public ActionResult Create(TABusinessLayer.Catalogue collection)
        {
           
                if (ModelState.IsValid)
                {
                    dbset.AddCatalogueToDB(collection);
                    return RedirectToAction("Index");
                }
                return View();
            
        }

        //
        // GET: /CatalogueMasterGroups/Edit/5

        public ActionResult Edit(int id)
        {
            TABusinessLayer.Catalogue ctitem = dbset.Catalogues.Single(emp => emp.catalogueId == id);
            if (ctitem == null)
            {
                return HttpNotFound();
            }

            return View(ctitem);
        }

        //
        // POST: /CatalogueMasterGroups/Edit/5

        [HttpPost]
        public ActionResult Edit(TABusinessLayer.Catalogue collection)
        {
            
                if (ModelState.IsValid)
                {
                    dbset.SaveChangesCatalogueToDB(collection);
                    return RedirectToAction("Index");
                }

                return View(collection);
            
              
        }

    
        
    }
}
