using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class DonutChartData
    {
        public string[] labels { get; set; }
        public int[] series { get; set; }
        public string[] colors { get; set; }
    }
}