using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuy_DB
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public void CreateProduct(string name, double price, int categoryID);
        public void UpdateProduct(int productID, string updatedName);
        public void DeleteProduct(int productID);
    }
}
