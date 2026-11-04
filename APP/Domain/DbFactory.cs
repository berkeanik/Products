using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace APP.Domain
{
    // Used for scaffolding and seeding the database with initial data.
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        const string CONNECTIONSTRING = "data source=DB.db";

        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlite(CONNECTIONSTRING);
            return new Db(optionsBuilder.Options);
        }

        public void SeedDb()
        {
            var db = CreateDbContext(null);

            // delete all the existing data first
            var productStores = db.ProductStores.ToList();
            db.ProductStores.RemoveRange(productStores);
            var products = db.Products.ToList();
            db.Products.RemoveRange(products);
            var stores = db.Stores.ToList();
            db.Stores.RemoveRange(stores);
            var categories = db.Categories.ToList();
            db.Categories.RemoveRange(categories);
            db.SaveChanges();

            // reset the auto increment ids
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='ProductStores'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Products'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Stores'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Categories'");

            db.Stores.Add(new Store { Name = "Ankara Store", IsVirtual = false });
            db.Stores.Add(new Store { Name = "Istanbul Store", IsVirtual = false });
            db.Stores.Add(new Store { Name = "Online Store", IsVirtual = true });
            db.SaveChanges();

            var ankaraStoreId = db.Stores.Single(s => s.Name == "Ankara Store").Id;
            var istanbulStoreId = db.Stores.Single(s => s.Name == "Istanbul Store").Id;
            var onlineStoreId = db.Stores.Single(s => s.Name == "Online Store").Id;

            db.Categories.Add(new Category
            {
                Title = "Computer",
                Description = "Laptops, desktops and accessories",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Laptop",
                        UnitPrice = 35000.50m,
                        StockAmount = 10,
                        StoreIds = new List<int> { ankaraStoreId, onlineStoreId }
                    },
                    new Product
                    {
                        Name = "Mouse",
                        UnitPrice = 450m,
                        StockAmount = 50,
                        StoreIds = new List<int> { ankaraStoreId, istanbulStoreId, onlineStoreId }
                    },
                    new Product
                    {
                        Name = "Keyboard",
                        UnitPrice = 900.75m,
                        StoreIds = new List<int> { istanbulStoreId }
                    }
                }
            });
            db.Categories.Add(new Category
            {
                Title = "Food",
                Description = "Food products",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Chocolate",
                        UnitPrice = 35.5m,
                        StockAmount = 200,
                        ExpirationDate = DateTime.Parse("12/31/2027", new CultureInfo("en-US")),
                        StoreIds = new List<int> { ankaraStoreId, istanbulStoreId }
                    },
                    new Product
                    {
                        Name = "Coffee",
                        UnitPrice = 250m,
                        StockAmount = 80,
                        ExpirationDate = new DateTime(2027, 6, 1),
                        StoreIds = new List<int> { onlineStoreId }
                    }
                }
            });
            db.Categories.Add(new Category { Title = "Book" });
            db.SaveChanges();
        }
    }
}
