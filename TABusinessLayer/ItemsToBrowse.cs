using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TABusinessLayer
{
    public class ItemsToBrowse
    {
        public decimal itemId { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public string itemPrice { get; set; }
        public decimal catalogueId { get; set; }
        public decimal sellerId { get; set; }

        public string itemPicture { get; set; }
        public string pictureA { get; set; }
        public string pictureB { get; set; }
        public string pictureC { get; set; }
        public string itemAlternatePicName { get; set; }
        public string sellerName { get; set; }
        public string sellerAddress { get; set; }
        public string sellerPhone { get; set; }
        public string sellerWebsite { get; set; }

       
        public string sellerEmail { get; set; }
    }
}
