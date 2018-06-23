using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrendAfriqOnline.Models;
using PagedList;
namespace TrendAfriqOnline.Controllers
{
    public class CatalogueController : Controller
    {

        TrendAfriqEntities ta = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();

        
        public ActionResult CatalogueList()
        {
            var genres = ta.Catalogues.OrderBy(a => a.catalogueName).ToList();
            return PartialView(genres);
        }

        public ActionResult CatalogueAdvert()
        {
            return PartialView();
        }

        public ActionResult Browse(decimal id, int? page)
        {
            //ViewBag.CurrentSort = sortOrder;
            //if (id != null)
            //   {
            //      page = 1;
            //   }
               //else
               //{
               //   searchString = currentFilter;
               //}

               //ViewBag.CurrentFilter = searchString;


            var fdgrp =  dbset.RetrieveItemsinCatalogueType(id); //storeDB.FoodItems.Include("QSSellerDetail").OrderBy(a => a.FoodName).Where(grs => grs.SubFoodGroupId == id && grs.QuantityAvailable > 0).ToList();
            //return View(fdgrp.ToList());
            if (fdgrp.ToList().Count > 0) 
                {
                    int pageSize = 20;
                    int pageNumber = (page ?? 1);

                    return View(fdgrp.ToPagedList(pageNumber, pageSize));
                }
            else { return View("ErrorEmptyGroup"); }
             
        }

        //
        // GET: /Catalogue/Details/5

        public ActionResult Details(int id)
        {
            var CatItem = ta.SellerItemss.Include("SellerDetails").Single(emp => emp.itemId == id);
            if (CatItem.itemId.ToString() == null)
            {
                return HttpNotFound();
            }
            return View(CatItem);
        }

        //
        // GET: /Catalogue/Create

        public ActionResult ContactSeller(int id)
        {
            var CatItem = ta.SellerItemss.Include("SellerDetails").Single(emp => emp.itemId == id);

            //send email
            TABusinessLayer.SendEmails sd = new TABusinessLayer.SendEmails();
            TABusinessLayer.SellerDetails det = new TABusinessLayer.SellerDetails();
            det.sellerAddress = CatItem.SellerDetails.sellerAddress;
            det.sellerEmail = CatItem.SellerDetails.sellerEmail;
            det.sellerName = CatItem.SellerDetails.sellerName;
            det.sellerPhone = CatItem.SellerDetails.sellerPhone;


            sd.InformSellerOfContactRequest(det);
            
            //Display Contact details
            if (CatItem == null)
            {
                return HttpNotFound();
            }
            return View(CatItem);
        }

        //
        // POST: /Catalogue/Create

        public ActionResult SearchItem(string search, int? page)
        {
           
            
                   var finalPosts = dbset.FindSearchRequest(search);
                
                if (finalPosts.ToList().Count > 0)
                {
                    int pageSize = 20;
                    int pageNumber = (page ?? 1);

                    return View("Browse", finalPosts.ToPagedList(pageNumber, pageSize));
                }
                else { return View("ErrorEmptyGroup"); }
            }
           
            //search code
            /*var posts = (from p in ctx.posts
    where words.Any(w => p.title.Contains(w))
    select p).ToList();*/
        }
    }

