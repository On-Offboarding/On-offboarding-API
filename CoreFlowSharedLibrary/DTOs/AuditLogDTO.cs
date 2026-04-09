using CoreFlowSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.DTOs
{
    public class AuditLogDTO
    {
        public AuditAction Action { get; set; }
        public int CaseId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
