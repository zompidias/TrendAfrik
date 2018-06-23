using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrendAfriqOnline.Models
{
    [Table("tblCatalogue")]
    public class Catalogue
    {
        [Key]
        public decimal catalogueId { get; set; }
        public string catalogueName { get; set; }
        public virtual List<SellerItems> SellerItems { get; set; }
    }
}

