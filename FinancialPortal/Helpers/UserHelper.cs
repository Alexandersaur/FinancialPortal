using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Helpers
{

    public class UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        public string GetFullName(string userId)
        {
            var user = db.Users.Find(userId);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            return firstName + " " + lastName;
        }

        public string GetFirstName(string userId)
        {
            var user = db.Users.Find(userId);
            var firstName = user.FirstName;
            return firstName;
        }

        public ApplicationUser getUser(string userId)
        {
            return db.Users.Find(userId);
        }

        public string LastNameFirst(string userId)
        {
            var user = db.Users.Find(userId);
            return user.FullName;
        }

        public string GetAvatarPath()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            return user.AvatarPath;
        }

        public string GetUserRole()
        {
            if (HttpContext.Current != null)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var roleId = user.Roles.Where(u => u.UserId == userId).FirstOrDefault().RoleId;
                var roleName = db.Roles.Find(roleId).Name;
                return roleName;
            }
            else
            {
                return "";
            }
        }
        public string GetUserRole(string userId)
        {
            return null;
        }
        public List<ApplicationUser> GetUserList()
        {
            return db.Users.ToList();
        }

        public List<ApplicationUser> GetUserList(string role)
        {
            return roleHelper.UsersInRole(role).ToList();
        }
    }

}