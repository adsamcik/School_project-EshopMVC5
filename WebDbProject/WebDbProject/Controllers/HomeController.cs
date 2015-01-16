using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDbProject.Models;
using WebDbProject.Models.Database;

namespace WebDbProject.Controllers {
    public class HomeController : Controller {
        //
        // GET: /Home/

        public ActionResult Index(string searchString, int filterCategory = -1) {
            Eshop.Models.Product[] p = ProductStorage.FindAllProducts();

            p = p.Where(v => v.Visible == true).ToArray();
            if (!String.IsNullOrEmpty(searchString)) {
                searchString = searchString.ToLower();
                p = p.Where(s => s.Title.ToLower().Contains(searchString) || s.Description.ToLower().Contains(searchString)).ToArray();
            }
            if(filterCategory > -1)
                p = p.Where(v => v.CategoryID == filterCategory).ToArray();

            ViewBag.categories = ProductStorage.GetCategories();
            ViewBag.filterCategory = filterCategory;

            return View(p);
        }

        public ActionResult Product(string ID) {
            int x = 0;
            if (Int32.TryParse(ID, out x)) {
                return View(ProductStorage.FindById(x));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult AddReview(string productID, string name, string plus, string minus) {
            int x = 0;
            if (Int32.TryParse(productID, out x)) {
                Review r = new Review();
                r.ReviewDate = DateTime.Now;
                r.Plus = plus;
                r.Minus = minus;
                r.Author = name;
                r.ProductID = x;
                ProductStorage.AddReview(r);
                return RedirectToAction("Product", "Home", new { ID = productID});
            }
            else return RedirectToAction("Index", "Home");
            
        }

    }
}
