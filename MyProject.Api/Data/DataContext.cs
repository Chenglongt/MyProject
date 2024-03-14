using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data.Entities;

namespace MyProject.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User>Users{ get; set; }
        public DbSet<Noodle>Noodles{ get; set; }
        public DbSet<NoodleOption>NoodleOptions{ get; set; }
        public DbSet<Order>Orders{ get; set; }
        public DbSet<OrderItem>OrderItems{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoodleOption>()
                .HasKey(io => new { io.NoodleId, io.Flavor, io.Topping });
            AddSeedData(modelBuilder);
        }

        private static void AddSeedData(ModelBuilder modelBuilder) 
        {
            Noodle[] noodles = 
                [
                new Noodle {Id = 1, Name = "Tonkotsu", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Tonkotsu.png",Price = 14},
                new Noodle {Id = 2, Name = "Geki Kara", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Geki%20Kara.png",Price =10.50},
                new Noodle {Id = 3, Name = "Chilli Chicken", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Chilli%20Chicken.png",Price =13.50 },
                new Noodle {Id = 4, Name = "Chilli Tiger Prawn", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Chilli%20Tiger%20Prawn.png",Price = 14.95},
                new Noodle {Id = 5, Name = "Tokyo ", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Tokyo.png",Price = 14.50},
                new Noodle {Id = 6, Name = "Japanese Mushroom Miso", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Japanese%20Mushroom%20Miso.png",Price =11.95 },
                new Noodle {Id = 7, Name = "Japanese Mushroom", Image = "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Japanese%20Mushroom.png",Price =11.95 },
                ];
            
            //Noodle Options****************************
            var toppings = new[] { "Egg", "Pork Belly", "Vegetable", "Chilli Chicken" };
            var flavors = new[] { "Default", "Extra Spicy", "Extra Solt" };

            var noodleOptions = new List<NoodleOption>();
            foreach (var noodle in noodles)
            {
                foreach (var topping in toppings)
                {
                    foreach (var flavor in flavors)
                    {
                        noodleOptions.Add(new NoodleOption
                        {
                            NoodleId = noodle.Id,
                            Topping = topping,
                            Flavor = flavor
                        });
                    }
                }
            }

            modelBuilder.Entity<Noodle>().HasData(noodles);
            modelBuilder.Entity<NoodleOption>().HasData(noodleOptions.ToArray());
        }

        
    }
}
