using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;

namespace ApiCore.Interfaces
{
    public interface IOrderService
    {
        Task<Order> MapPostOrderViewModel(PostOrderViewModel orderModel);
        List<GetOrderViewModel> MapGetOrderViewModel(List<Order> order);
        List<GetOrderViewModel> ApplyFiltersIfAny(List<GetOrderViewModel> viewModels, GetOrdersFilter filter);
    }
}
