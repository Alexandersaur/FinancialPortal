using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class UpdateProfileVM
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        public string LastName { get; set; }

        public UpdateProfileVM(ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
        public UpdateProfileVM()
        {

        }

    }
}