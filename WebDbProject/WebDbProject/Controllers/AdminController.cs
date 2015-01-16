using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDbProject.Models;

namespace WebDbProject.Controllers {
    public class AdminController : Controller {
        // GET: Admin
        public ActionResult Index() {
            return View();
        }

        public ActionResult ProductList() {
            return View(ProductStorage.FindAllProducts());
        }

        public ActionResult CategoryList() {
            return View(ProductStorage.GetCategories());
        }

        public ActionResult ReviewList()
        {
            return View(ProductStorage.GetAllReviews());
        }

        public ActionResult DeleteProduct(string id) {
            int x = 0;
            if (Int32.TryParse(id, out x)) {
                ProductStorage.Delete(x);
                return RedirectToAction("ProductList", "Admin");
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteCategory(string id) {
            int x = 0;
            if (Int32.TryParse(id, out x)) {
                ProductStorage.DeleteCategory(x);
                return RedirectToAction("CategoryList", "Admin");
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteReview(string id) {
            int x = 0;
            if (Int32.TryParse(id, out x)) {
                ProductStorage.DeleteReview(x);
                return RedirectToAction("ReviewList", "Admin");
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditProduct(string id) {
            int x = 0;
            if (Int32.TryParse(id, out x)) {
                ViewBag.categories = ProductStorage.GetCategories();
                return View(ProductStorage.FindById(x));
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditCategory(string id) {
            int x = 0;
            if (Int32.TryParse(id, out x)) {
                return View(ProductStorage.GetCategory(x));
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult AddProduct() {
            ViewBag.categories = ProductStorage.GetCategories();
            return View();
        }
        public ActionResult AddCategory() {
            return View();
        }

        [HttpPost]
        public ActionResult Update(Product p, string Category) {
            p.CategoryID = ProductStorage.categories.Find(x => x.Name == Category).CategoryID;
            ProductStorage.Update(p);
            return RedirectToAction("ProductList", "Admin");
        }

        [HttpPost]
        public ActionResult Add(Product p, string Category) {
            p.CategoryID = ProductStorage.categories.Find(x => x.Name == Category).CategoryID;
            ProductStorage.Add(p);
            return RedirectToAction("ProductList", "Admin");
        }

        [HttpPost]
        public ActionResult AddCategory(Category c) {
            ProductStorage.AddCategory(c);
            return RedirectToAction("CategoryList", "Admin");
        }

        [HttpPost]
        public ActionResult EditCategory(Category c) {
            ProductStorage.UpdateCategory(c);
            return RedirectToAction("CategoryList", "Admin");
        }
    }
}