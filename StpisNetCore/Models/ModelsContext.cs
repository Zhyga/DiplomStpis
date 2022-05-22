using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options)
        {

        }
        public DbSet<tblProducts> products { get; set; }
        public DbSet<tblClients> clients { get; set; }
        public DbSet<tblRole> role{ get; set; }
        public DbSet<tblOrders> orders { get; set; }
        public DbSet<tblStatus> status{ get; set; }
        public DbSet<tblWarehouse> warehouses{ get; set; }
        public DbSet<tblVehicle> vehicle{ get; set; }
        public DbSet<tblRegions> region{ get; set; }
        public DbSet<tblDeliveries> deliveries { get; set; }
    }
}
