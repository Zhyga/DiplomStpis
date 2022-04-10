using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Client ID")]
        public int ClientId { get; set; }

        [Display(Name = "Product Name")]
        public int ProductId { get; set; }

        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        [Display(Name = "Product Amount")]
        public int ProductAmount { get; set; }
    }
}
