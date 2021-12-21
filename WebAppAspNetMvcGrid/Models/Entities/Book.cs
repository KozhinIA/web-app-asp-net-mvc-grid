using Common.Attributes;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAspNetMvcGrid.Models
{
    public class Book
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>    
        [Required]
        [Display(Name = "Название", Order = 5)]
        public string Name { get; set; }
                
        /// <summary>
        /// ISBN
        /// </summary>  
        [Required]
        [Display(Name = "ISBN", Order = 20)]
        public string Isbn { get; set; }

        /// <summary>
        /// Год издания книги
        /// </summary>  
        [Display(Name = "Год издания книги", Order = 30)]
        public int Year { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>  
        [Display(Name = "Стоимость", Order = 40)]
        public decimal Cost { get; set; }

        /// <summary>
        /// Тип валюты
        /// </summary> 
        [ScaffoldColumn(false)]
        public int CurrencyTypeId { get; set; }
        [ScaffoldColumn(false)]
        public virtual CurrencyType CurrencyType { get; set; }

        [Display(Name = "Тип валюты", Order = 45)]
        [UIHint("RadioList")]
        [TargetProperty("CurrencyTypeId")]
        [NotMapped]
        public IEnumerable<SelectListItem> CurrencyTypeDictionary
        {
            get
            {
                var db = new LibraryContext();
                var query = db.CurrencyTypes;

                if (query != null)
                {
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == CurrencyTypeId));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }
        /// <summary>
        /// Дата создания записи
        /// </summary> 
        [ScaffoldColumn(false)]
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Жанр
        /// </summary> 
        [ScaffoldColumn(false)]
        public int GenreId { get; set; }
        [ScaffoldColumn(false)]
        public virtual Genre Genre { get; set; }

        [Display(Name = "Жанр", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("GenreId")]
        [NotMapped]
        public IEnumerable<SelectListItem> GenreDictionary
        {
            get
            {
                var db = new LibraryContext();
                var query = db.Genres;

                if (query != null)
                {
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == GenreId));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }


        /// <summary>
        /// Обложка книги
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual BookImage BookImage { get; set; }

        [Display(Name = "Изображение обложки книги", Order = 60)]
        [NotMapped]
        public HttpPostedFileBase BookImageFile { get; set; }

        /// <summary>
        /// Авторы
        /// </summary> 
        [ScaffoldColumn(false)] 
        public virtual ICollection<Author> Authors { get; set; }


        [ScaffoldColumn(false)]
        public List<int> AuthorIds { get; set; }

        [Display(Name = "Авторы", Order = 70)]
        [UIHint("MultipleDropDownList")]
        [TargetProperty("AuthorIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> AuthorDictionary
        {
            get
            {
                var db = new LibraryContext();
                var query = db.Authors;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Books.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.LastName} {c.FirestName}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }


        /// <summary>
        /// Архивная запись
        /// </summary>  
        [Display(Name = "Архивная запись", Order = 100)]
        public bool IsArchive { get; set; }

        /// <summary>
        /// Описание
        /// </summary>    
        [Required]
        [Display(Name = "Аннотация", Order = 200)]
        [UIHint("TextArea")]
        public string Annotation { get; set; }

        /// <summary>
        /// Ключ для создания/изменения записи
        /// </summary>    
        [Required]
        [Display(Name = "Ключ для создания/изменения записи", Order = 1000)]
        [UIHint("Password")]
        [NotMapped]
        public string Key { get; set; }


        /// <summary>
        /// Языки
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Language> Languages { get; set; }

        [ScaffoldColumn(false)]
        public List<int> LanguageIds { get; set; }

        [Display(Name = "Языки", Order = 55)]
        [UIHint("MultipleSelect")]
        [TargetProperty("LanguageIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> LanguageDictionary
        {
            get
            {
                var db = new LibraryContext();
                var query = db.Languages;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Books.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.Name}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }
    }
}