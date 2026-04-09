using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
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
        public int? CaseId { get; set; }
        public CaseDTO? Case { get; set; }

        public UserDTO? User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
