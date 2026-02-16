# On-offboarding-API
Backend-API för On/Offboarding-lösningen.
 
> **Teknik i korthet**
> - ASP.NET Core (.NET 8)
> - Autentisering via Microsoft Identity Web / Azure AD (JWT Bearer) (ej påslaget än)
> - Swagger/OpenAPI i Development
> - SQL Server (default: LocalDB) med init i Development

### Förkrav
- .NET SDK 8
- (Valfritt) Visual Studio 2022 / Rider / VS Code
- SQL Server LocalDB (om du kör default-setup) eller annan SQL Server-instans

- ### Installera & bygg
```bash
git clone https://github.com/On-Offboarding/On-offboarding-API.git
cd On-offboarding-API
dotnet restore
dotnet build
