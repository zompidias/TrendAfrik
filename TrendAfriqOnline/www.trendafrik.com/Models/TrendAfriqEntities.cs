using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrendAfriqOnline.Models
{
    public class TrendAfriqEntities : DbContext
    {
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<SellerDetails> SellerDetailss { get; set; }
        public DbSet<SellerItems> SellerItemss { get; set; }
    }
}