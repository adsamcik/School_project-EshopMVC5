using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public string Plus { get; set; }
        public string Minus { get; set; }
        public string Author { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}