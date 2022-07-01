using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace BestBuy_DB
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection; //field

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                                "VALUES (@name, @price, @categoryID);"
                , new
                {
                    name = name,
                    price = price,
                    categoryID = categoryID
                });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            Thread.Sleep(3000);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void UpdateProduct(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new
                {
                    updatedName = updatedName,
                    productID = productID
                });
            Console.WriteLine("Product Updated");
            Thread.Sleep(3000);
        }

















        //private readonly IDbConnection _connection; //field
        //public ProductRepository()
        //{
        //    _connection = connection;
        //}

        //public void CreateProduct(string name, double price, int categoryID)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return _connection.Query<Products>("SELECT * FROM Products;");
        //}
    }
}
