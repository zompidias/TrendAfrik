using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TABusinessLayer;
using TrendAfriqOnline.Models;


namespace TrendAfriqOnline.Controllers
{
    public class ContactUsController : Controller
    {
        TrendAfriqEntities db = new TrendAfriqEntities();
        TABusinessLayer.CatalogueBusinessLayer dbset = new TABusinessLayer.CatalogueBusinessLayer();

        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ContactUs/Create

        [HttpPost]
        public ActionResult Create(TABusinessLayer.ContactUs frmCollection)
        {
            if (ModelState.IsValid)
            {
                // try
                //{
                // TODO: Add insert logic here
                dbset.AddEnquiryToDB(frmCollection);

                //Send information to Administrator email
                TABusinessLayer.SendEmails em = new TABusinessLayer.SendEmails();
                em.SendEnquiryToEmail(frmCollection);
                return RedirectToAction("Index");
                //}
                //catch
                //{
                // return View();
                //}
            }
            return View();
        }

        //
    }
}
