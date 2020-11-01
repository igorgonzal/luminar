using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scoredCard.Models
{
    public class Scorecard
    {
        public int Id { get; set; }
        public string UNITID { get; set; }
        public string CITY { get; set; }
        public string ZIP { get; set; }
        public string INSTNM { get; set; }
        public string REGION { get; set; }
    }
}
