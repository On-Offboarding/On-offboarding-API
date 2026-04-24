using AutoMapper;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CoreFlowAPI.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeRepository(IDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteEmployeesAccordingToGDPR(DateTime? endDate = null)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                await connection.QueryFirstOrDefaultAsync<User>(
                    "Update Employees set PersonalIdLastDigits = '0000', PhoneNumber = STUFF(PhoneNumber, LEN(PhoneNumber)-1, 2, 'XX') where isnull(EndDate,getdate()) <= @EndDate", new { EndDate = endDate });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var sql = @"SELECT 
                e.Id,
                e.FirstName,
                e.LastName,
                e.Title,
                e.PersonalId,
                e.PersonalIdLastDigits,
                e.PhoneNumber, 
                e.Company,
                e.Department,
                e.StartDate,
                e.EndDate,
                e.DateOfEmployment,
                e.UserId,
                a.Id as AccountId,
                a.UserName,
                a.Info,
                a.SystemAccessId,
                a.Status,
                a.EmployeeId
                FROM Employees e
                Left Join Accounts a On a.EmployeeId = e.Id";

            var employeeDictionary = new Dictionary<int, EmployeeDTO>();
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<Employee, Account, EmployeeDTO>(
               sql, (employee, account) =>
               {
                   if (!employeeDictionary.TryGetValue(employee.Id, out var currentEmployee))
                   {
                       currentEmployee = _mapper.Map<EmployeeDTO>(employee);
                       currentEmployee.Accounts = new List<AccountDTO>();
                       employeeDictionary.Add(employee.Id, currentEmployee);

                   }

                   if (account != null && account.SystemAccessId != 0)
                   {
                       currentEmployee.Accounts.Add(_mapper.Map<AccountDTO>(account));
                   }

                   return currentEmployee;
               },
                splitOn: "AccountId"
                    );
            return employeeDictionary.Values.ToList();
        }

        public async Task<EmployeeDTO?> GetByIdAsync(int id)
        {
            var sql = @"SELECT 
                e.Id,
                e.FirstName,
                e.LastName,
                e.Title,
                e.PersonalId,
                e.PersonalIdLastDigits,
                e.PhoneNumber, 
                e.Company,
                e.Department,
                e.StartDate,
                e.EndDate,
                e.DateOfEmployment,
                e.UserId,
                a.Id as AccountId,
                a.UserName,
                a.Info,
                a.SystemAccessId,
                a.Status,
                a.EmployeeId
                FROM Employees e
                Left Join Accounts a On a.EmployeeId = e.Id
                where e.Id = @Id";

            var employeeDictionary = new Dictionary<int, EmployeeDTO>();
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<Employee, Account, EmployeeDTO>(
               sql, (employee, account) =>
               {
                   if (!employeeDictionary.TryGetValue(employee.Id, out var currentEmployee))
                   {
                       currentEmployee = _mapper.Map<EmployeeDTO>(employee);
                       currentEmployee.Accounts = new List<AccountDTO>();
                       employeeDictionary.Add(employee.Id, currentEmployee);

                   }

                   if (account != null && account.SystemAccessId != 0)
                   {
                       currentEmployee.Accounts.Add(_mapper.Map<AccountDTO>(account));
                   }

                   return currentEmployee;
               },
                new { Id = id },
                splitOn: "AccountId"
                    );
            return employeeDictionary.Values.FirstOrDefault();
        }
    }
}
