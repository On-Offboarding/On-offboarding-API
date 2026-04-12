using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Enums
{
    public enum AuditAction
    {   
        Unknown = 0, 
        [Display(Name = "{0} skapad",Description = "{0} för {1} {2} skapad")]
        CaseCreated = 1,
        [Display(Name = "{0} uppdaterad", Description = "{0} för {1} {2} uppdaterad")]
        CaseUpdated = 2,
        [Display(Name = "{0} avslutad", Description = "{0} för {1} {2} avslutad")]
        CaseCompleted = 3,
    }
}
