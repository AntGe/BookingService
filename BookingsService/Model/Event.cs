using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{
    public class Event
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; } 
        public ICollection<Booking> Bookings { get; set; }
    }
}
