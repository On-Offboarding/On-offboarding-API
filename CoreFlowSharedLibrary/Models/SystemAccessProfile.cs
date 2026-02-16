using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Models
{
    public class SystemAccessProfile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<SystemAccess> SystemAccesses { get; set; } = new();
    }
}
