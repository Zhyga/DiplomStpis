using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StpisNetCore.Models
{
    public class tblClients
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        public string Password { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Column("phone_number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Column("role_id")]
        [Display(Name = "RoleId")]

        public int Role_Id { get; set; }

        [NotMapped]
        public tblRole Role { get; set; }

        [NotMapped]
        public List<tblRole> RolesList { get; set; }
    }
}
