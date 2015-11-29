using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.ModelLibrary
{
    public class StatusUtil
    {
        public enum Status
        {
            Active,
            InActive
        }

        public enum RequestType
        {
            WithSerialNumber = 1,
            WithoutSerialNumber
        }

        public enum CardType
        {
            MasterCard = 1,
            Verve
        }

        public enum CardStatus
        {
            Requested = 1,
            Approved,
            Printed
        }
    }
}
