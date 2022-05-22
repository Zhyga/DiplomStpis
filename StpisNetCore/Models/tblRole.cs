using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblRole
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Role Name")]
        public string Role { get; set; }
    }
}
