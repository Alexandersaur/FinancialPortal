﻿using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Helpers
{
    public class RoleHelper
    {
        private UserManager<ApplicationUser> userManager = new
        UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new
        ApplicationDbContext()));

        private ApplicationDbContext db = new ApplicationDbContext();
        public string GetUserRole()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var roleId = user.Roles.Where(u => u.UserId == userId);
            return null;
        }
        public string GetUserRole(string userId)
        {
            return null;
        }
        public string GetFullName(string userId)
        {
            var user = db.Users.Find(userId);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            return firstName + " " + lastName;
        }
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public ICollection<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }
        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }
        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }
        public bool  UpdateUserRole(string userId, string roleName)
        {
            var currentRoles = ListUserRoles(userId);
            if(currentRoles.Count != 0)
            {
                foreach(var role in currentRoles)
                {
                    RemoveUserFromRole(userId, role);
                }
            }
            return AddUserToRole(userId, roleName);
        }
        public ICollection<ApplicationUser> UsersInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }
            return resultList;
        }
        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }
            return resultList;
        }
        public int GetPercentPaid(Budget budget)
        {
            if (budget.TargetAmount == 0)
            {
                return 0;
            }
            var percentPaid = (int)decimal.Round((budget.CurrentAmount / budget.TargetAmount) * 100, 0);

            if (percentPaid > 100)
            {
                percentPaid = 100;
            }
            return percentPaid;
        }
    }
}