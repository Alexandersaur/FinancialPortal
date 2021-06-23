using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class ResultItem
    {
        public string ItemName { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }
    }
}