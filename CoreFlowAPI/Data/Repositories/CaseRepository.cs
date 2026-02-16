using AutoMapper;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
using Dapper;

namespace CoreFlowAPI.Data.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public CaseRepository(IDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CaseDTO @case)
        {
            using var connection = _dbContext.CreateConnection();

            var sql = @"
             INSERT INTO dbo.Employees (FirstName,LastName,Title,PersonalId,PersonalIdLastDigits,PhoneNumber,Company,
             Department,StartDate,EndDate,DateofEmployment,UserId)
             OUTPUT INSERTED.Id
             VALUES (@FirstName,@LastName,@Title,@PersonalId,@PersonalIdLastDigits,@PhoneNumber,@Company,
             @Department,@StartDate,@EndDate,@DateofEmployment,@UserId);
             ";

            var employeeId = await connection.QuerySingleAsync<int>(sql, new
            {
                FirstName = @case.Employee.FirstName,
                LastName = @case.Employee.LastName,
                Title = @case.Employee.Title.ToString(),
                PersonalId = @case.Employee.PersonalId.Split('-').First(),
                PersonalIdLastDigits = @case.Employee.PersonalId.Split('-').Last(),
                PhoneNumber = @case.Employee.PhoneNumber,
                Company = @case.Employee.Company.ToString(),
                Department = @case.Employee.Department,
                StartDate = @case.Employee.StartDate,
                EndDate = @case.Employee.EndDate,
                DateOfEmployment = @case.Employee.DateOfEmployment,
                UserId = @case.CreatedByUser,
            });



             sql = @"
             INSERT INTO dbo.Cases (Type,Status,EmployeeId,CreatedByUser)
             OUTPUT INSERTED.Id
             VALUES (@Type, @Status, @EmployeeId, @CreatedByUser);
             ";

            var caseId = await connection.QuerySingleAsync<int>(sql, new
            {
                Type = (int)@case.Type,
                Status = (int)@case.Status,
                EmployeeId = employeeId,
                CreatedByUser = @case.CreatedByUser
            });
            foreach (var account in @case.Employee.Accounts) 
            {
                sql = @"
                INSERT INTO dbo.Accounts (UserName,Info,SystemAccessId,Status,EmployeeId)
                OUTPUT INSERTED.Id
                VALUES (@UserName, @Info, @SystemAccessId, @Status,@EmployeeId);
                ";

                 await connection.QuerySingleAsync<int>(sql, new
                {
                    UserName = account.UserName,
                    Info = account.Info,
                    SystemAccessId = account.SystemAccessId,
                    Status = account.Status,
                    EmployeeId = employeeId
                 });
            }
            

            return caseId;

        }

        public async Task<IEnumerable<CaseDTO>> GetAllAsync()
        {
            var sql = @"SELECT 
                c.Id,
                c.Type,
                c.Status,
                c.CreatedByUser,
                c.EmployeeId,
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
                a.Id,
                a.Id as AccountId,
                a.UserName,
                a.Info,
                a.SystemAccessId,
                a.Status,
                a.EmployeeId
                FROM Cases c
                Inner Join Employees e On c.EmployeeId = e.Id
                Left Join Accounts a On a.EmployeeId = e.Id";

            var caseDictionary = new Dictionary<int, CaseDTO>();
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<Case,Employee,Account,CaseDTO>(
               sql,(obj,employee,account) =>
                { 
                    if (!caseDictionary.TryGetValue(obj.Id,out var currentCase)) 
                    {
                        currentCase = _mapper.Map<CaseDTO>(obj);
                        currentCase.Employee = _mapper.Map<EmployeeDTO>(employee);
                        currentCase.Employee.Accounts = new List<AccountDTO>();
                        caseDictionary.Add(obj.Id, currentCase);
                        
                    }
                    
                    if (account != null && account.SystemAccessId != 0)
                    {
                        currentCase.Employee.Accounts.Add(_mapper.Map<AccountDTO>(account));
                    }
                    
                    return currentCase;
                },
                splitOn:"EmployeeId,AccountId"
                    );
            return caseDictionary.Values.ToList();
        }

        public async Task<CaseDTO?> GetByIdAsync(int id)
        {
            var sql = @"SELECT 
                c.Id,
                c.Type,
                c.Status,
                c.CreatedByUser,
                c.EmployeeId,
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
                a.Id,
                a.Id as AccountId,
                a.UserName,
                a.Info,
                a.SystemAccessId,
                a.Status,
                a.EmployeeId
                FROM Cases c
                inner join Employees e On c.EmployeeId = e.Id
                left join Accounts a On a.EmployeeId = e.Id
                where c.Id = @caseId";

            var caseDictionary = new Dictionary<int, CaseDTO>();
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<Case, Employee, Account, CaseDTO>(
               sql, (obj, employee, account) =>
               {
                   if (!caseDictionary.TryGetValue(obj.Id, out var currentCase))
                   {
                       currentCase = _mapper.Map<CaseDTO>(obj);
                       currentCase.Employee = _mapper.Map<EmployeeDTO>(employee);
                       currentCase.Employee.Accounts = new List<AccountDTO>();
                       caseDictionary.Add(obj.Id, currentCase);

                   }

                   if (account != null && account.SystemAccessId != 0)
                   {
                       currentCase.Employee.Accounts.Add(_mapper.Map<AccountDTO>(account));
                   }

                   return currentCase;
               },
               new { CaseId = id },
                splitOn: "EmployeeId,AccountId"
                    );
            return caseDictionary.Values.FirstOrDefault();
        }
    }
}
