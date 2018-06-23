﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrendAfriqOnline.Models
{
    [Table("tblSellerItems")]
    public class SellerItems
    {
        [Key]
        public decimal itemId { get; set; }
        public string itemName {get; set;}
        public string itemDescription {get; set;}
        public string itemPrice {get; set;}
        public decimal catalogueId {get; set;}
        public decimal sellerId {get; set;}
       public string itemPicture { get; set; }
       public string pictureA { get; set; }
       public string pictureB { get; set; }
       public string pictureC { get; set; }
        public string itemAlternatePicName { get; set; }
        public virtual Catalogue Catalogue { get; set; }
        public virtual SellerDetails SellerDetails { get; set; }
    }

}


