using CoreFlowSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Info { get; set; }
        public int SystemAccessId { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }

    }
}
