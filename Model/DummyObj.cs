using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DummyObj
    {
        public string TransactionId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public bool Valid { get; set; }
        public DateTime validUntil { get; set; }
        public string LocatorId { get; set; }
    }
}
