using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KioskSolution.Models
{
    public class CardRequestModel
    {
        public long CustomerID { get; set; }
        public long CardTypeID { get; set; }
        public long PickupBranchID { get; set; }
        public string Username { get; set; }
        public long RequestTypeID { get; set; }
        public string SerialNumber { get; set; }
    }
}