using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Auditable:BaseEntity
    {
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
