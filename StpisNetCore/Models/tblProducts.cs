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

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Size")]
        public int Size { get; set; }
    }
}
