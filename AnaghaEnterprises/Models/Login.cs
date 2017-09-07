using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AnaghaEnterprises.Models;
using AnaghaEnterprises.Helper;

namespace AnaghaEnterprises.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Role { get; set; }

        public bool IsAuthorize()
        {
            var adminobj = new AdminModel() {
                UserName = this.UserName,
                Password = CryptoHelper.Encrypt(this.Password)
            };
             adminobj = adminobj.getAdmin();
            if (adminobj != null && adminobj.AdminId > 0)
            {
                this.Role = adminobj.RoleName;
                return true;
            }
            return false;
        }
    }
}