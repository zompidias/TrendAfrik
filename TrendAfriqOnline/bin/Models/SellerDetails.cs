using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrendAfriqOnline.Models
{
    [Table("tblSellerDetails")]
    public class SellerDetails
    {
        [Key]
        public decimal sellerId { get; set; }
        public string sellerName { get; set; }
        public string sellerAddress { get; set; }
        public string sellerPhone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not Valid")]

        public string sellerEmail { get; set; }
         [DataType(DataType.Date)]
        public DateTime sellerExpiryDate { get; set; }
         public string sellerWebsite { get; set; }
        public virtual List<SellerItems> SellerItems { get; set; }
    }
}