using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblRegions
    {
        public int Id{ get; set; }

        [Column("region_name")]
        public string RegionName{ get; set; }
    }
}
