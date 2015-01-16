using Eshop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using WebDbProject.Models.Database;

namespace WebDbProject.Models {
    public static class ProductStorage {
        static TestContext ctx = new TestContext();
        static bool local = true;

        static List<Product> _products;
        public static List<Product> products { get { if (local) return _products; else return ctx.Products.ToList(); } set { _products = value; } }

        static List<Review> _reviews;
        public static List<Review> reviews { get { if (local) return _reviews; else return ctx.Reviews.ToList(); } set { _reviews = value; } }

        static List<Category> _categories;
        public static List<Category> categories { get { if (local) return _categories; else return ctx.Category.ToList(); } set { _categories = value; } }
        static string jsonPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/");

        static ProductStorage() {
            Debug.WriteLine(jsonPath);
            StreamReader sr = new StreamReader(jsonPath + "Products.txt");
            products = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd());
            sr.Close();

            sr = new StreamReader(jsonPath + "Reviews.txt");
            reviews = JsonConvert.DeserializeObject<List<Review>>(sr.ReadToEnd());
            sr.Close();

            sr = new StreamReader(jsonPath + "Categories.txt");
            categories = JsonConvert.DeserializeObject<List<Category>>(sr.ReadToEnd());
            sr.Close();
        }

        public static Product[] FindAllJSON() {
            return products.ToArray();
        }

        public static Product[] FindAllProducts() {
            Product[] products;
            if (local) products = FindAllJSON();
            else products = ctx.Products.ToArray<Product>();
            return products.OrderByDescending(x => x.ProductID).ToArray();
        }

        static Category[] GetCategoriesJSON() {
            return categories.ToArray();
        }

        public static Category[] GetCategories() {
            if (local) return GetCategoriesJSON();
            return categories.ToArray();
        }

        public static Category GetCategory(int categoryID) {
            if (local) return categories.Find(x => x.CategoryID == categoryID);
            return categories.Find(x => x.CategoryID == categoryID);
        }

        public static Review[] GetAllReviews() {
            return reviews.OrderByDescending(x => x.ReviewDate).ToArray();
        }

        public static Review[] FindAllReviewsFor(int productID) {
            if (local) return FindAllReviewsForJSON(productID);
            return ctx.Reviews.Where(x => x.ProductID == productID).ToArray();
        }

        static Review[] FindAllReviewsForJSON(int productID) {
            return reviews.FindAll(x => x.ProductID == productID).ToArray();
        }

        public static Product FindById(int id) {
            Product p;
            if (local) 
                p = products.FirstOrDefault(x => x.ProductID == id);
            else
                p = (from Product r in ctx.Products where r.ProductID == id select r).FirstOrDefault<Product>();

            p.Reviews = FindAllReviewsFor(id).OrderByDescending(x => x.ReviewDate.TimeOfDay).ToArray();
            return p;
        }

        static void UpdateJSON(Product product) {
            products[products.FindIndex(x => x.ProductID == product.ProductID)] = product;
            Save("Products.txt", products);
        }
        public static void Update(Product product) {
            if (local) { UpdateJSON(product); return; }
            Product p = FindById(product.ProductID);

            //p = product;

            p.CategoryID = product.CategoryID;
            p.Description = product.Description;
            p.Image = product.Image;
            p.Price = product.Price;
            p.ProductID = product.ProductID;
            p.Reviews = product.Reviews;
            p.Stock = product.Stock;
            p.Title = product.Title;
            p.Vat = product.Vat;
            p.Visible = product.Visible;

            ctx.SaveChanges();
        }

        public static void DeleteCategory(int id) {
            Category c = GetCategory(id);
            if (local) {
                categories.Remove(c);
                Save("Categories.txt", categories);
            }
            else {
                ctx.Category.Remove(c);
                ctx.SaveChanges();
            }
        }

        public static void DeleteReview(int id) {
            Review r = GetAllReviews().First(x => x.ReviewID == id);
            if (local) {
                reviews.Remove(r);
                Save("Reviews.txt", reviews);
            }
            else {
                ctx.Reviews.Remove(r);
                ctx.SaveChanges();
            }
        }

        public static void Delete(int id) {
            Product p = FindById(id);
            if (local) {
                products.Remove(p);
                Save("Products.txt", products);
            }
            else {
                ctx.Products.Remove(p);
                ctx.SaveChanges();
            }
        }

        static void AddJSON(Product p) {
            p.ProductID = products.OrderByDescending(x => x.ProductID).ToArray()[0].ProductID;
            products.Add(p);
            Save("Products.txt", products);
        }

        public static void Add(Product p) {
            p.Vat = 19;
            if (local) { AddJSON(p); return; }
            ctx.Products.Add(p);
            ctx.SaveChanges();
        }

        static void AddCategoryJSON(Category c) {
            c.CategoryID = categories.Count;
            categories.Add(c);
            Save("Categories.txt", categories);
        }

        public static void AddCategory(Category c) {
            if (local) { AddCategoryJSON(c); return; }
            ctx.Category.Add(c);
            ctx.SaveChanges();
        }

        public static void AddReview(Review r) {
            if (local) {
                r.ReviewID = reviews.OrderByDescending(x => x.ReviewID).ToArray()[0].ReviewID + 1;
                reviews.Add(r);
                Save("Reviews.txt", reviews);
            }
            else {
                ctx.Reviews.Add(r);
                ctx.SaveChanges();
            }
        }

        static void UpdateCategoryJSON(Category category) {
            categories[categories.FindIndex(x => x.CategoryID == category.CategoryID)] = category;
            Save("Categories.txt", categories);
        }

        public static void UpdateCategory(Category category) {
            if (local) UpdateCategoryJSON(category);
            Category c = GetCategory(category.CategoryID);

            c.Name = category.Name;
            ctx.SaveChanges();
        }


        static void Save<T>(string file, List<T> list) {
            StreamWriter sw = new StreamWriter(jsonPath + file);
            sw.Write(JsonConvert.SerializeObject(list));
            sw.Close();
        }
    }
}