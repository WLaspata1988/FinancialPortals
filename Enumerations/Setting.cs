using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FinancialPortals.Enumerations
{
    public enum Setting
    {
        DefaultAvatar,
        DefaultPassword,
        SplitCharacter,
        SeededHouseName,
        HeadOfHouse,
        SeededAccountTypeC,
        SeededAccountTypeS,
        BankAccountName,
        TransactionTypeD,
        TransactionTypeW,
        [Description("By creating your house you have automatically ")]
        DefaultNotificationBody,       
        SeededHeadOfHouse,
        SeededHouseGreeting
        
    }
}