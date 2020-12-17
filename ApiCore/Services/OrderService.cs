using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;
using ApiCore.Helpers;
using ApiCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly StoreDbContext _context;

        public OrderService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Order> MapPostOrderViewModel(PostOrderViewModel orderModel)
        {
            var userToAddOrder = await _context.Users.FindAsync(orderModel.UserId);
            var allProductIdsInDb = await _context.Products.Select(x => x.Id).ToListAsync();

            if (userToAddOrder == null)
                throw new Exception(Message.OrderUserNotFound);

            if(!orderModel.ProductsIds.Any())
                throw new Exception(Message.NoProductsIds);

            if (!orderModel.ProductsIds.All(x => allProductIdsInDb.Contains(x)))
                throw new Exception(Message.ProductsNotFound);

            var order = new Order()
            {
                OrderProducts = new List<OrderProduct>(),
                UserId = userToAddOrder.Id
            };

            foreach (var productId in orderModel.ProductsIds)
            {
                order.OrderProducts.Add(new OrderProduct()
                {
                    OrderId = order.Id,
                    ProductId = productId
                });
            }

            return order;
        }

        public List<GetOrderViewModel> MapGetOrderViewModel(List<Order> orders)
        {
            var models = new List<GetOrderViewModel>();

            foreach (var order in orders)
            {
                var products = order.OrderProducts
                    .Select(x => x.Product)
                    .GroupBy(x => new { x.Name })
                    .Select(p => new ProductViewModel()
                    {
                        Name = p.Key.Name,
                        Quantity = p.Count(),
                        Price = p.Sum(x => x.Price)
                    })
                    .ToList();

                var model = new GetOrderViewModel()
                {
                    Id = order.Id,
                    TotalValue = order.OrderProducts.Sum(p => p.Product.Price),
                    Products = products,
                    CreationDateTime = order.CreationDateTime
                };

                models.Add(model);
            }

            return models;
        }

        public List<GetOrderViewModel> ApplyFiltersIfAny(List<GetOrderViewModel> viewModels, GetOrdersFilter filter)
        {
            var filteredModels = viewModels.Where(x =>
                        x.TotalValue >= filter.MinPrice &&
                        x.TotalValue <= filter.MaxPrice &&
                        x.CreationDateTime >= filter.MinDateTime &&
                        x.CreationDateTime <= filter.MaxDateTime)
                .ToList();

            return filteredModels;
        }
    }
}
