using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSD.Entities
{
    public class StoreShort
    {
        public int Id { get; set; }
        public string BookingNo { get; set; }
        public string Serial { get; set; }
        public string Row { get; set; }
        public string ItemNo { get; set; }
        public string StoreBLNo { get; set; }
        public string Status { get; set; }
        public string GrossWeight { get; set; }
        public string BuyerName { get; set; }
        public string ExStoreBLSerial { get; set; }
        public string IssueDate { get; set; }
        public string DischargeDate { get; set; }
        public string Comments { get; set; }
}
}