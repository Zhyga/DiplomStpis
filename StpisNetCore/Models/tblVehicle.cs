using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblVehicle
    {
        public int Id { get; set; }

        public int Capacity { get; set; }

        [Column("warehouse_id")]
        public int WarehouseID{ get; set; }
    }
}
