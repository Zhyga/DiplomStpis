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

        [Display(Name = "Логин")]
        public string Login { get; set; }

        public string Password { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Column("phone_number")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Column("role_id")]
        [Display(Name = "Роль")]

        public int Role_Id { get; set; }

        [NotMapped]
        public tblRole Role { get; set; }

        [NotMapped]
        public List<tblRole> RolesList { get; set; }
    }
}
