using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;

namespace ApiCore.Interfaces
{
    public interface IProductService
    {
        void MapChanges(Product product, PutProductModel productModel);
    }
}
