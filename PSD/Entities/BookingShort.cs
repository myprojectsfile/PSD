using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSD.Entities
{
    public class BookingShort
    {
        public int BookingId { get; set; }
        public string Booking { get; set; }
        public string VesselName { get; set; }
        public string Status { get; set; }
        public string ShipAgent { get; set; }
        public string SubDate { get; set; }
    }
}