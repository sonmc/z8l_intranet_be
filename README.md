# Z8l_Tool API

Z8l.Api/
├── Dependencies/
├── Property/
| └── launchSettings.json
|
├── Controllers/
│ ├── GeneralController.cs
| └── Authentication.cs
|
├── MiddleWares/
| └── JwtMiddleware.cs
|
└── appsetting.json
└── program.cs

# Environment Dependency

-   Dotnet core V6
-   Dotnet ef
-   Pomelo.EntityFrameworkCore.MySql
-   Microsoft.EntityFrameworkCore.Relational
-   System.IdentityModel.Tokens.Jwt
-   Microsoft.AspNetCore.Authentication.JwtBearer

## Command to generate db used to EF Code first

-   dotnet ef migrations add FirstMigration
-   dotnet ef database update

## Init

-   for capture all router of api endpoint
    -- {DOMAIN}/api/options/upgrade_enpoints
