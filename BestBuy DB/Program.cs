using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace BestBuy_DB
{
    internal class Program
    {
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        static string connString = config.GetConnectionString("DefaultConnection");

        static IDbConnection conn = new MySqlConnection(connString);


        static void Main(string[] args)
        {
            Console.WriteLine("What would you like to do? (Imput number)\n");
            Console.WriteLine("1. Display the list of departments");
            Console.WriteLine("2. Display the list of products.");
            Console.WriteLine("3. Create new department.");
            Console.WriteLine("4. Create new product.");
            Console.WriteLine("5. Update product.");
            Console.WriteLine("6. Delete product.");
            Console.WriteLine();
            
            var UserImput = Convert.ToInt32(Console.ReadLine());

            switch (UserImput)
            {
                case 1:
                    listDepartment();     // Display the list of departments
                    break;
                case 2:
                    createDepartment();   // Create new department.
                    break;
                case 3:
                    listProduct();        // Display the list of products.
                    break;
                case 4:
                    createProduct();      // Create new product.
                    break;
                case 5:
                    UpdateProductName();  // Update product.
                    break;
                case 6:
                    DeleteProduct();      // Delete product.
                    break;
                default: Console.WriteLine("Invalid imput, please enter a number");
                    break;
            }


            //listDepartment();     // Display the list of departments

            //createDepartment();   // Create new department.

            //listProduct();        // Display the list of products.

            //createProduct();      // Create new product.

            //UpdateProductName();  // Update product.

            //DeleteProduct();      // Delete product.

        }
        public static void DeleteProduct()
        {
            //ProductRepository instance - so we can call our dapper methods
            var prodRepo = new ProductRepository(conn);

            //User interaction
            Console.WriteLine($"Please enter the productID of the product you would like to delete:");
            var productID = Convert.ToInt32(Console.ReadLine());

            //Call the Dapper method
            prodRepo.DeleteProduct(productID);
        }
        public static void UpdateProductName()
        {
            var prodRepo = new ProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
            var updatedName = Console.ReadLine();

            prodRepo.UpdateProduct(productID, updatedName);
        }
        public static void listDepartment()
        {

            string connString = config.GetConnectionString("DefaultConnection");
            var repo1 = new DepartmentRepository(conn);

            Console.WriteLine($"LISTING ALL DEPARTMENTS:");
            var departments = repo1.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }

            Console.WriteLine();
        }
        public static void createDepartment()
        {
            string connString = config.GetConnectionString("DefaultConnection");
            var repo1 = new DepartmentRepository(conn);
            var departments = repo1.GetAllDepartments();

            Console.WriteLine("What is the name for the new department?");
            var deptName = Console.ReadLine();

            repo1.InsertDepartment(deptName);
            departments = repo1.GetAllDepartments();

            Console.WriteLine();

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }
            Console.WriteLine();
        }
        public static void listProduct()
        {

            string connString = config.GetConnectionString("DefaultConnection");

            var repo2 = new ProductRepository(conn);

            var product = repo2.GetAllProducts();
            Console.WriteLine("LISTING ALL PRODUCTS");
            foreach (var prod in product)
            {
                Console.WriteLine($"Product ID: {prod.ProductID}\nName: {prod.Name}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void createProduct()
        {
            var productRepo = new ProductRepository(conn);
            Console.WriteLine("ADDING NEW PRODUCT\n");
            Console.WriteLine("What is the product name?\n");
            var name = Console.ReadLine();

            Console.WriteLine("What is the product price?\n");
            var price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("What is the product categoryID?\n");
            var categoryID = Convert.ToInt32(Console.ReadLine());

            productRepo.CreateProduct(name, price, categoryID);

            var products = productRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"Product Name: {prod.Name} \nPrice: {prod.Price} \nCategoryID: {prod.CategoryID}");
                Console.WriteLine();
            }
        }

    }
}
