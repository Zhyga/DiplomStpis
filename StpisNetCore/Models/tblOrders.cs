using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblOrders
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int OrderNumber { get; set; }

        public int ProductAmount { get; set; }
    }
}
