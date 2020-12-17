using System;
using System.Collections.Generic;

namespace ApiCore.Data.EntityModels
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}
