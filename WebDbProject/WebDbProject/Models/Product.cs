using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public double Price { get; set; }
        public double Vat { get; set; }
        public int Stock { get; set; }
        public virtual Review[] Reviews { get; set; }
    }
}