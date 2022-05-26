using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblProducts
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Amount { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Размер")]
        public int Size { get; set; }
    }
}
