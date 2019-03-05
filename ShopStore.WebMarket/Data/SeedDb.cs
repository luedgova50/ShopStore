namespace ShopStore.WebMarket.Data
{    
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;

        private readonly Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;

            random = new Random();
        }

        public async Task SeedAsync()
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Products.Any())
            {
                AddProduct("Plana Singer 190 D");
                AddProduct("Collarin 670");
                AddProduct("Fileteadora 965");
                AddProduct("Familiar Singer 550");
                await context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            context.Products.Add(new Product
            {
                Name = name,
                Price = random.Next(2500000),
                IsAvailabe = true,
                Stock = random.Next(100)
            });
        }

    }
}
