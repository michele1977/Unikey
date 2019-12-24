using System.Linq;
using System.Web.Mvc;
using MasterDetail.Business;
using MasterDetail.Models;
using MasterDetail.Models.DTO;

namespace MasterDetail.Controllers
{
    public class AuthController : Controller
    {
        readonly AuthorService _authorService = new AuthorService();

        [HttpGet]
        public ActionResult Index()
        {
            var myModel = new AuthIndexModel {Authors = _authorService.GetAllAuthors()
                .Select(x => new AuthorDTO(x)).ToList()};
            return View(myModel);
        }

        [HttpGet]
        public ActionResult ViewBooksDetail5(int authorId)
        {
            var myModel = new AuthIndexModel
            {
                Authors = _authorService.GetAllAuthors().Select(x => new AuthorDTO(x)).ToList(),
                SelectedBooks = _authorService.GetAllBooksOfAuthor(authorId)
                    .Select(x => new BookDTO(x)).ToList()
            };
            return View("Index", myModel);
        }
        [HttpGet]
        public ActionResult ViewBooksDetail(int authorId)
        {
            var selectedBooks = _authorService.GetAllBooksOfAuthor(authorId)
                .Select(x => new BookDTO(x)).ToList();
            return PartialView("BookPartial", new BookPartialModel(){Books = selectedBooks });
        }


        // GET: Auth/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Auth/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Auth/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Auth/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Auth/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Auth/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Auth/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
