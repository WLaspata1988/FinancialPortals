using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.ViewModel
{
    public class ChangeUserInfoViewModel
    {
        public string ConfirmedEmail { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UnConfirmedEmail { get; set; }

        [MaxLength(50, ErrorMessage = "First Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string NewFirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string NewLastName { get; set; }

        [MaxLength(20, ErrorMessage = "Display Name cannot be greater than 20 characters")]
        [Display(Name = "Display Name")]
        public string NewDisplayName { get; set; }

        [MaxLength(50, ErrorMessage = "First Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string OldFirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string OldLastName { get; set; }

        [MaxLength(20, ErrorMessage = "Display Name cannot be greater than 20 characters")]
        [Display(Name = "Display Name")]
        public string OldDisplayName { get; set; }
    }
}
