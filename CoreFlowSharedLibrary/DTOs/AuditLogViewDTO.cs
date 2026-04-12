using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.DTOs
{
    public class AuditLogViewDTO
    {
        public string? Title { get; set; }   
        public string? Description { get; set; }
        public string? ByUser { get; set; }
        public string? Time { get; set; }
    }
}
