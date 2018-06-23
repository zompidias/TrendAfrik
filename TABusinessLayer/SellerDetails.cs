using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TABusinessLayer
{
   public class SellerDetails
    {
        public decimal sellerId { get; set; }
        public string sellerName { get; set; }
        public string sellerAddress { get; set; }
        public string sellerPhone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not Valid")]
        public string sellerWebsite { get; set; }
        public string sellerEmail { get; set; }
        public DateTime sellerExpiryDate { get; set; }
        public string daysLeftToExpire { get; set; }
        public string AcctNumber { get; set; }
        public string AcctName { get; set; }
        public string AcctBank { get; set; }
        public string AdminPhone { get; set; }
        public string SubscriptionFee { get; set; }
        public virtual List<SellerItems> SellerItems { get; set; }
    }
}
