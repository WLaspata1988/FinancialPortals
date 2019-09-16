using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.Enumerations
{
    public enum Role
    {        
            Admin,
            [Display(Name ="Head Of Household")]
            HeadOfHousehold,
            Lobbyist,
            Member
    }
}