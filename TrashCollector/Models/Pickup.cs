using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickup
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string EployeeId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public double Cost { get; set; }

    }
}