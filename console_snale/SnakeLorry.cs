using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_snale
{
    internal class SnakeLorry
    {
        public CoOrdinate Head { get; set; }
        public int Length { get; set; }
        public List<CoOrdinate> Tail { get; set; }
    }
    internal class CoOrdinate
    {
        public int XLocation { get; set; }
        public int YLocation { get; set; }
    }
}
