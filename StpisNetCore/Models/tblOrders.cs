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
        [Display(Name = "ID Клиента")]
        public int ClientId { get; set; }

        [Column("product_id")]
        [Display(Name = "Название товара")]
        public int ProductId { get; set; }

        [Column("order_number")]
        [Display(Name = "Номер заказа")]
        public int OrderNumber { get; set; }

        [Column("product_amount")]
        [Display(Name = "Количество товара")]
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
