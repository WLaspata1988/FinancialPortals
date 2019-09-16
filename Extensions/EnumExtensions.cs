using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FinancialPortals.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            switch(value)
            {
                case null: return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : "There is no description for this item.";
                default: return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(3)).Description : "This text is for me, and me alone.";
            }
            
        }
    }
}