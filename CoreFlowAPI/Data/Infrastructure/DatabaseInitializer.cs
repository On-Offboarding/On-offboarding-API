using Dapper;
using Microsoft.Data.SqlClient;

namespace CoreFlowAPI.Data.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static async Task InitAsync(IConfiguration config)
        {
            await EnsureDatabaseExists(config);
            await EnsureTablesExist(config);
            await InsertSeedData(config);
        }

        private static async Task InsertSeedData(IConfiguration config)
        {
            await using var conn =
            new SqlConnection(config.GetConnectionString("AppDb"));

            var sql = """
        IF NOT EXISTS(select top 1 * from dbo.SystemAccesses)
        BEGIN 
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Office 365');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('CreditSafe');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('UC');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Coface');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Allianz');
                INSERT INTO dbo.SystemAccess(Name)
                VALUES('HubSpot');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Scrive');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Metabase');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Zapier');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Databox');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Ekopost');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Keeros');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Norde Corp.Netbank');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Finansia App');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Fortnox Integration');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Rival (AgeraPay)');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Google Ads');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Facebook Pages');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Instagram Business');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Zendesk');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Intercom');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Tellit Växel/Telefoni');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Bria Teams');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Tellit Tech (Telefoni)');   
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Dinumero');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Servebolt');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Azure');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Heroku');
                INSERT INTO dbo.SystemAccess(Name)
                VALUES('Hightouch');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Finansia.se (Wordpress)');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Teamtailor');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('KÄK');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Callmaker');
                INSERT INTO dbo.SystemAccesses(Name)
                VALUES('Keeros faktura scanner');

        END    

        """;
            await conn.ExecuteAsync(sql);

            sql = """
        IF NOT EXISTS(select top 1 * from dbo.SystemAccessProfile)
        BEGIN 
                INSERT INTO dbo.SystemAccessProfile(Name)
                VALUES('Säljare');
        INSERT INTO dbo.SystemAccessProfile(Name)
                VALUES('Handläggare');
        END    

        """;
            await conn.ExecuteAsync(sql);

            sql = """
        IF NOT EXISTS(select top 1 * from dbo.ProfileSystemAccess )
        BEGIN 
                INSERT INTO dbo.ProfileSystemAccess (ProfileId,SystemAccessId)
                VALUES(1,2);
        INSERT INTO dbo.ProfileSystemAccess (ProfileId,SystemAccessId)
                VALUES(2,4);
        END    

        """;
            await conn.ExecuteAsync(sql);

            sql = """
        IF NOT EXISTS(select top 1 * from dbo.Roles)
        BEGIN 
                INSERT INTO dbo.Roles(Name)
                VALUES('Admin');
        INSERT INTO dbo.Roles(Name)
                VALUES('Chef');
        END    

        """;
            await conn.ExecuteAsync(sql);

            sql = """
        IF NOT EXISTS(select top 1 * from dbo.Users)
        BEGIN 
                INSERT INTO dbo.Users(Name, Email, RoleId)
                VALUES('Casper Caspersson','cat@hotmail.com', 1);
        END    

        """;
            await conn.ExecuteAsync(sql);
        }

        private static async Task EnsureTablesExist(IConfiguration config)
        {
            await using var conn =
              new SqlConnection(config.GetConnectionString("AppDb"));

            var sql = CreateUsersTable();
            await conn.ExecuteAsync(sql);

            sql = CreateSystemAccessTable();
            await conn.ExecuteAsync(sql);

            sql = CreateRoleTable();
            await conn.ExecuteAsync(sql);

            sql = CreateEmployeeTable();
            await conn.ExecuteAsync(sql);

            sql = CreateCaseTable();
            await conn.ExecuteAsync(sql);

            sql = CreateAccountTable();
            await conn.ExecuteAsync(sql);

            sql = CreateProfileTable();
            await conn.ExecuteAsync(sql);

            sql = CreateProfileSystemAccessTable();
            await conn.ExecuteAsync(sql);
        }

        

        private static async Task EnsureDatabaseExists(IConfiguration config)
        {
            await using var conn =
           new SqlConnection(config.GetConnectionString("master"));

            var sql = """
        IF DB_ID('CoreFlowDb') IS NULL
        BEGIN
            CREATE DATABASE CoreFlowDb;
        END
        """;

            await conn.ExecuteAsync(sql);
        }

        private static string CreateUsersTable()
        {
            return """
        IF OBJECT_ID('dbo.Users', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Users (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100) NOT NULL,
                Email NVARCHAR(100) NOT NULL,
                RoleId INT NOT NULL
            );
        END
        """;
        }

        private static string CreateProfileTable()
        {
            return """
        IF OBJECT_ID('dbo.SystemAccessProfile', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.SystemAccessProfile (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100) NOT NULL
            );
        END
        """;
        }

        private static string CreateProfileSystemAccessTable()
        {
            return """
        IF OBJECT_ID('dbo.ProfileSystemAccess ', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.ProfileSystemAccess  (
                ProfileId INT NOT NULL,
                SystemAccessId INT NOT NULL
                    PRIMARY KEY (ProfileId, SystemAccessId),

                    FOREIGN KEY (ProfileId) REFERENCES SystemAccessProfile(Id),
                    FOREIGN KEY (SystemAccessId) REFERENCES SystemAccesses(Id)
            );
        END
        """;
        }

        private static string CreateAccountTable()
        {
            return """
        IF OBJECT_ID('dbo.Accounts', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Accounts (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                UserName NVARCHAR(50) NULL,
                Info NVARCHAR(MAX) NULL,
                SystemAccessId INT NULL,
                Status INT NOT NULL,
                EmployeeId INT NOT NULL
            );
        END
        """;
        }

        private static string CreateCaseTable()
        {
            return """
        IF OBJECT_ID('dbo.Cases', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Cases (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Type INT NOT NULL,
                Status INT NOT NULL,
                EmployeeId INT NOT NULL,
                CreatedByUser INT NOT NULL
            );
        END
        """;
        }

        private static string CreateEmployeeTable()
        {
            return """
        IF OBJECT_ID('dbo.Employees', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Employees (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                FirstName NVARCHAR(50) NULL, 
                LastName NVARCHAR(50) NULL,
                Title NVARCHAR(50) NULL, 
                PersonalId INT NOT NULL, 
                PersonalIdLastDigits INT NOT NULL,
                PhoneNumber NVARCHAR(50) NULL,
                Company NVARCHAR(50),
                Department NVARCHAR(50),
                StartDate DATETIME NOT NULL,
                EndDate DATETIME NULL,
                DateOfEmployment DATETIME NOT NULL, 
                UserId INT NOT NULL
            );
        END
        """;
        }

        private static string CreateRoleTable()
        {
            return """
        IF OBJECT_ID('dbo.Roles', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Roles (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100) NOT NULL
            );
        END
        """;
        }

        private static string CreateSystemAccessTable()
        {
            return """
        IF OBJECT_ID('dbo.SystemAccesses', 'U') IS NULL
        BEGIN
            CREATE TABLE dbo.SystemAccesses (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100) NOT NULL,
                Description NVARCHAR(100) NULL
            );
        END
        """;
        }

    }
}
