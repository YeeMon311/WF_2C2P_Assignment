using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ManageTransaction.Entities
{
    public class Transactions
    {

        public int ID = 0;
        public string TransactionID = string.Empty;
        public double Amount = 0.00;
        public string CurrencyCode = string.Empty;
        public DateTime TransactionDate;
        public string Status = string.Empty;
        public int CurrencyID = 0;

    }

    public class Currencies
    {
        public int ID = 0;
        public string CountryCode = string.Empty;
        public string CurrencyCode = string.Empty;
        public string CurrencyDescription = string.Empty;
        public string CreatedDate = string.Empty;
    }


    public class Countries
    {
        public int ID = 0;
        public string CountryCode = string.Empty;
        public string CountryName = string.Empty;

    }
}