using CoreFlowSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public required string Action { get; set; }
        public int? CaseId { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
