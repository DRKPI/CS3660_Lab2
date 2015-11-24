using Lab1_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab1_BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            List<BookStoreModel> list = DatabaseQueries.ReadAllBooks();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Lab 2: ASP.NET MVC and CRUD";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Lab 2 contact page.";

            return View();
        }
        public ActionResult Details(int id)
        {
            BookStoreModel book = DatabaseQueries.ReadBook(id);
            return View(book);
        }

        // GET: BookStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookStore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                BookStoreModel book = new BookStoreModel();

                book.Title = collection["Title"];
                book.Author = collection["Author"];
                book.PublishedDate = DateTime.Parse(collection["PublishedDate"]);
                book.Cost = double.Parse(collection["Cost"]);
                book.InStock = collection["InStock"] != "false";
                book.BindingType = collection["BindingType"];
                //book.Cover = collection["Cover"];

                //Do client-side validation
                if (!ModelState.IsValid)
                {
                    return View(book);
                }

                DatabaseQueries.CreateRecord(book);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                    "Error commiting info to database: " + ex.Message);
            }
        }

        // GET: BookStore/Edit/5
        public ActionResult Edit(int id)
        {
            BookStoreModel book = DatabaseQueries.ReadBook(id);
            return View(book);
        }

        // POST: BookStore/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var test = int.Parse(collection["Id"]);
                // TODO: Add update logic here
                BookStoreModel book = new BookStoreModel();
                book.Id = int.Parse(collection["Id"]);
                book.Title = collection["Title"];
                book.Author = collection["Author"];
                book.PublishedDate = DateTime.Parse(collection["PublishedDate"]);
                book.Cost = double.Parse(collection["Cost"]);
                book.InStock = collection["InStock"] != "false";
                book.BindingType = collection["BindingType"];
                //book.Cover = collection["Cover"];

                //Do client-side validation
                if (!ModelState.IsValid)
                {
                    return View(book);
                }

                DatabaseQueries.UpdateRecord(book);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                    "Error commiting info to database: " + ex.Message);
            }
        }

        // GET: BookStore/Delete/5
        public ActionResult Delete(int id)
        {
            BookStoreModel book = DatabaseQueries.ReadBook(id);
            return View(book);
        }

        // POST: BookStore/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Do client-side validation
                if (!ModelState.IsValid)
                {
                    return View();
                }

                // TODO: Add delete logic here
                DatabaseQueries.DeleteBook(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                    "Error commiting info to database: " + ex.Message);
            }
        }    
    }
}