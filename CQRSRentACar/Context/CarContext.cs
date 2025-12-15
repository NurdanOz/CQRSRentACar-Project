using CQRSRentACar.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.Context
{
    public class CarContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=*****;initial catalog=CQRSRentACarDb;integrated security=true;trust server certificate=true");
        }

        public   DbSet<About> Abouts { get; set; }
        public   DbSet<Booking> Bookings { get; set; }
        public   DbSet<Car> Cars { get; set; }
        public   DbSet<Employee> Employees { get; set; }
        public   DbSet<Feature> Features { get; set; }
        public   DbSet<Location> Locations { get; set; }
        public   DbSet<Message> Messages { get; set; }
        public   DbSet<Service> Services { get; set; }
        public   DbSet<Slider> Sliders { get; set; }
        public   DbSet<Testimonial> Testimonials { get; set; }
    }
}
