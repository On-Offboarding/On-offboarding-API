using CoreFlowSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Models
{
    public class Case
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public int CreatedByUser { get; set; }
        public required List<Account> Accounts { get; set; } = new List<Account>(); 
    }
}
