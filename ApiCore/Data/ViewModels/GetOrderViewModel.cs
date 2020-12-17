using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Data.ViewModels
{
    public class GetOrderViewModel
    {
        public int Id { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
