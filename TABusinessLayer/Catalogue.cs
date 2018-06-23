using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TABusinessLayer
{
    public class Catalogue
    {
        public decimal catalogueId { get; set; }
        public string catalogueName { get; set; }
        public virtual List<SellerItems> SellerItems { get; set; }
    }
}
