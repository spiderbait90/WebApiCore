using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;
using ApiCore.Interfaces;

namespace ApiCore.Services
{
    public class ProductService : IProductService
    {
        public void MapChanges(Product product, PutProductModel productModel)
        {
            if (productModel.Name != null)
                product.Name = productModel.Name;

            if (productModel.Description != null)
                product.Description = productModel.Description;

            if (productModel.Price != null)
                product.Price = (decimal) productModel.Price;
        }
    }
}
