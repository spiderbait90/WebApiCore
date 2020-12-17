using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCore.Data;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;
using ApiCore.Helpers;
using ApiCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ApiCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController(StoreDbContext context, IMapper mapper, IOrderService orderService)
        {
            _context = context;
            _mapper = mapper;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderViewModel>>> GetOrders(GetOrdersFilter filter)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var orders = await _context.Orders
                    .Where(x => x.UserId == currentUserId)
                    .Include(x => x.OrderProducts)
                    .ThenInclude(x => x.Product)
                    .ToListAsync();

                if (!orders.Any())
                {
                    return NotFound();
                }

                var viewModels = _orderService.MapGetOrderViewModel(orders);
                viewModels = _orderService.ApplyFiltersIfAny(viewModels, filter);

                return viewModels;
            }
            catch
            {
                return BadRequest(new { message = Message.LoggedInUserNotFound });
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderViewModel>> GetOrder(int id)
        {
            var orders = await _context.Orders
                .Where(x => x.Id == id)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            if (!orders.Any())
            {
                return NotFound();
            }

            var viewModel = _orderService.MapGetOrderViewModel(orders).First();

            return viewModel;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PostOrderViewModel orderModel)
        {
            try
            {
                var order = await _orderService.MapPostOrderViewModel(orderModel);

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
