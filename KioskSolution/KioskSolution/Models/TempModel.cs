using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KioskSolution.Models
{
    public class TempModel
    {
        public long CardRequestID { get; set; }
        public string RequestType { get; set; }
        public string LoggedInUsername { get; set; }
    }
}