using System;
using System.Collections.Generic;

namespace CarDealershipCapstone.Models
{
    public partial class Cars
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
