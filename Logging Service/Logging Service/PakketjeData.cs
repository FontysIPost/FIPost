using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging_Service
{
    public class PakketjeData
    {
         public int Id { get; set; }
        public string personID { get; set; }
        public string Pakketname { get; set; }
        public string ReceiverID { get; set; }
        public string Sender { get; set; }
        public string LocationID { get; set; }
        public DateTime Time { get; set; }
    }
}
