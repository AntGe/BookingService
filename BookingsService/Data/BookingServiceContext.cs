using BookingService.Data.EntityConfigurations;
using BookingService.Model;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Data
{
    public class BookingServiceContext : DbContext
    { 
        public BookingServiceContext()
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookingEntityConfiguration());
            builder.ApplyConfiguration(new EventEntityConfiguration()); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql.data;Initial Catalog=BookingService;Integrated Security=false;User Id=sa;Password=Pass@word;");
        }
    } 
}
