using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblOrders
    {
        [Key]
        public int Id { get; set; }

        [Column("client_id")]
        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        [Column("product_id")]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }

        [Column("order_number")]
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        [Column("product_amount")]
        [Display(Name = "Product Amount")]
        public int ProductAmount { get; set; }

        [NotMapped]
        public tblProducts Product { get; set; }

        [NotMapped]
        public tblClients Client { get; set; }

        [NotMapped]
        public List<tblProducts> ProductList{ get; set; }

        [NotMapped]
        public List<tblClients> ClientList { get; set; }
    }
}
