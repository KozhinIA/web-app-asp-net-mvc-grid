using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebAppAspNetMvcGrid.Models
{
    public class CurrencyType
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

        [Required]
        [Display(Name = "Буквенный код", Order = 15)]
        public string LetterCode { get; set; }

        [Required]
        [Display(Name = "Числовой код", Order = 25)]
        public string NumericCode { get; set; }


        /// <summary>
        /// Список книг
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Book> Books { get; set; }
    }
}