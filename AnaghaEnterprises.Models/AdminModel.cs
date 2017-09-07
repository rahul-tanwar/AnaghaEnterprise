using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnaghaEnterprises.DataAccess;

namespace AnaghaEnterprises.Models
{
  public class AdminModel
    {
      public int AdminId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string RoleName { get; set;}

        public AdminModel getAdmin()
        {
            var result = new AdminModel();
            using (var context = new AnaghaEnterprisesEntities())
            {
                result = (from item in context.Admins
                          join item1 in context.roles
                          on item.RoleId equals item1.RoleId
                          where item.UserName == this.UserName && item.Password == this.Password && item.IsActive == true
                          select new AdminModel
                          {
                              AdminId = item.AdminId,
                              UserName = item.UserName,
                              Password = item.Password,
                              Email = item.Email,
                              Name = item.Name,
                              IsActive = item.IsActive,
                              RoleId = item.RoleId,
                              CreatedDate = item.CreatedDate,
                              RoleName = item1.RoleName
                              
                          }).FirstOrDefault();

               
            }
            return result;
           
            }

        public String GetRole( string username)
        {
            using (var context = new AnaghaEnterprisesEntities())
            {
                var result =  (from item in context.Admins
                              join item1 in context.roles
                              on item.RoleId equals item1.RoleId
                              where item.UserName.Equals(username)
                              select item1.RoleName).FirstOrDefault();
                return result;
            }
        }

        }
    }

