using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppAspNetMvcGrid.Models;

namespace WebAppAspNetMvcGrid.Controllers
{
    public class CurrencyTypesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new LibraryContext();
            return View(db.Set<CurrencyType>());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var currencyType = new CurrencyType();
            return View(currencyType);
        }

        [HttpPost]
        public ActionResult Create(CurrencyType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new LibraryContext();                      

            db.CurrencyTypes.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/CurrencyTypes/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new LibraryContext();
            var currencyType = db.CurrencyTypes.FirstOrDefault(x => x.Id == id);
            if (currencyType == null)
                return RedirectPermanent("/CurrencyTypes/Index");

            db.CurrencyTypes.Remove(currencyType);
            db.SaveChanges();

            return RedirectPermanent("/CurrencyTypes/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new LibraryContext();
            var currencyType = db.CurrencyTypes.FirstOrDefault(x => x.Id == id);
            if (currencyType == null)
                return RedirectPermanent("/CurrencyTypes/Index");

            return View(currencyType);
        }

        [HttpPost]
        public ActionResult Edit(CurrencyType model)
        {
            var db = new LibraryContext();
            var currencyType = db.CurrencyTypes.FirstOrDefault(x => x.Id == model.Id);
            if (currencyType == null)
                ModelState.AddModelError("Id", "Жанр не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingCurrencyType(model, currencyType);

            db.Entry(currencyType).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/CurrencyTypes/Index");
        }

        private void MappingCurrencyType(CurrencyType sourse, CurrencyType destination)
        {
            destination.Name = sourse.Name;
            destination.LetterCode = sourse.LetterCode;
            destination.NumericCode = sourse.NumericCode;
        }
    }
}