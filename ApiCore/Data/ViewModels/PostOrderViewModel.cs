using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;

namespace ApiCore.Data.ViewModels
{
    public class PostOrderViewModel
    {
        [Required]
        public int UserId { get; set; }

        public List<int> ProductsIds { get; set; }
    }
}
