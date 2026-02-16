using CoreFlowSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.DTOs
{
    public class AccountDTO
    {
        public string UserName { get; set; }
        public string Info { get; set; }
        public int SystemAccessId { get; set; }
        public StatusOfAccount Status { get; set; }
    }
}
