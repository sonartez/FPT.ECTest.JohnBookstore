# FPT.ECTest.JohnBookstore
WebAPI for Interview

> This Api will be responsible for overall data distribution and authorization.
> This Application create use ASP.NET Core WebAPI Clean Architecture extension for Visual Studio [more details](https://marketplace.visualstudio.com/items?itemName=MukeshMurugan.CleanArchitectureWebApi)

**version**
-  ASP.NET Core 3.1 WebApi

**Configuration**
- Clone this repo and open FPT.ECTest.JohnBookstore.sln with Visual Studio 
- Open up WebAPI/appsettings.json to change the connection strings. Here you can choose to have multiple DBs for a separation of the IdentityDB or have the same DB for Identity and Application. Once that is Set, Run these commands to update the database.
> update-database -Context IdentityContext

> update-database -Context ApplicationDbContext

- Finally, build and run the Application

**Feature**
> Follow requirements of assignment, I create a Web API with some features

![FPT.ECTest.JohnBookstore](/resources/screenshot.JPG)

### Requirement APIs
- API for Get all books 
- API for find books with text input
- API for get a book detail by use ISBNCode
- API for place a order and update number of available books instock
- 
### Additional APIs
- API for create new shop
- API for import books to system with a xmlfile. Sample file for testing here [Books.xml](https://github.com/sonartez/FPT.ECTest.JohnBookstore/blob/main/resources/Books.xml)

### Other APIs
- APIs for Identity services
- APIs for get Meta Info

**Testing**
- API Version = 1.0
- Default users.
   > Email - superadmin@gmail.com / Password - JohnBookstore@2021

User /api/Account/authenticate API to get JWT token for authorization



