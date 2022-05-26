using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblDeliveries
    {

        public int Id { get; set; }

        [Column("delivery_Date")]
        [Display(Name = "Дата доставки")]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate{ get; set; }

        [Column("order_Date")]
        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; }

        [Column("status_id")]
        [Display(Name = "Статус")]
        public int StatusId{ get; set; }

        [Column("region_id")]
        [Display(Name = "Регион")]
        public int RegionId { get; set; }

        [Column("order_id")]
        [Display(Name = "Номер заказа")]
        public int OrderId { get; set; }

        [Column("warehouse_id")]
        [Display(Name = "Номер склада")]
        public int WarehouseId { get; set; }

        [NotMapped]
        public tblStatus Status { get; set; }

        [NotMapped]
        public tblRegions Region { get; set; }

        [NotMapped]
        public List<tblOrders> OrdersList{ get; set; }

        [NotMapped]
        public List<tblStatus> StatusList{ get; set; }

        [NotMapped]
        public List<tblRegions> RegionsList{ get; set; }

        [NotMapped]
        public List<tblWarehouse> WarehousesList{ get; set; }
    }
}
