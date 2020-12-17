using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Data.ViewModels
{
    public class GetOrdersFilter
    {
        public decimal MinPrice { get; set; } = decimal.MinValue;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public DateTime MinDateTime { get; set; } = DateTime.MinValue;
        public DateTime MaxDateTime { get; set; } = DateTime.MaxValue;
    }
}
