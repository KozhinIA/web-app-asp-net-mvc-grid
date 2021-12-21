using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcGrid.Models;

namespace WebAppAspNetMvcGrid.Controllers
{
    public class AuthorsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new LibraryContext();
            return View(db.Set<Author>());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var author = new Author();
            return View(author);
        }

        [HttpPost]
        public ActionResult Create(Author model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new LibraryContext();

            db.Authors.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new LibraryContext();
            var author = db.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return RedirectPermanent("/Authors/Index");

            db.Authors.Remove(author);
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new LibraryContext();
            var author = db.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return RedirectPermanent("/Authors/Index");

            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Author model)
        {
            var db = new LibraryContext();
            var author = db.Authors.FirstOrDefault(x => x.Id == model.Id);
            if (author == null)
                ModelState.AddModelError("Id", "Книга не найдена");

            if (!ModelState.IsValid)
                return View(model);

            MappingAuthor(model, author);

            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Authors/Index");
        }

        private void MappingAuthor(Author sourse, Author destination)
        {
            destination.FirestName = sourse.FirestName;
            destination.LastName = sourse.LastName;
            destination.Birthday = sourse.Birthday;
            destination.Gender = sourse.Gender;
        }
    }
}