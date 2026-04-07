using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Enums
{
    public enum AuditAction
    {
        None = 0,
        CaseCreated = 1,
        CaseUpdated = 2,
        CaseCompleted = 3,
        EmployeeCreated = 4,
        EmployeeUpdated = 5,
        EmployeeDeleted = 6,
        AccountsCreated = 7,
        AccountsUpdated = 8,
        UserCreated = 9,
        UserUpdated = 10,
        UserLoggedIn = 11,
        UserLoggedOut = 12,
    }
}
