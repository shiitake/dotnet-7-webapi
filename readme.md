# Dotnet 7 WebAPI

## Details
I didn't want to complicate the solution by over-engineering everything but I did want to keep the structure clean. 

I create base 3 projects
- Core - holds the domain entities 
- Application - has service layer and db connection 
- Web - contains the api endpoints

After running the db script on my localdb I used entity framework to scaffold the entities based on database tables.  I ran the following from the Package Manager Console in visual studio: 

```
PM> Scaffold-DbContext -Provider Microsoft.EntityFrameworkCore.SqlServer -Connection "Server=(localdb)\mssqllocaldb;Database=School;Trusted_Connection=True;" -Context SchoolDbContext -ContextDir . -ContextNamespace Infrastructure -Namespace Core.Domain.Entities -OutputDir ..\Core\Domain\Entities 
```

## Running the application 
By default the connection string is pointing to `(localdb)\mssqllocaldb`. To point it to a different database server you will need to adit the connection string in `.\src\Web\appsettings.json`

To start the api you can run the following:
```
dotnet run --project .\src\Web\Web.csproj
```

From here you can hit the api on `http://localhost:5215/Students`

You can also navigate to swagger here:  `http://localhost:5215/Swagger/index.html`

## Tests
Unit tests can be found in the tests folder. 

Test cases are specific to the service layer written using Xunit and Moq. 

You can run the unit tests in Visual Studio or from the command line by running:

```
> dotnet test .\tests\UnitTest.Application.Services\
```

## Challenge2

The `MaxDistance` solution can be found in `.\src\Web\Challenge2.cs`.


## Timeline

It took me about two hours to get the API setup and pulling data from the database. It took another hour for me to write the unit tests. 

For the MaxDistance solution it took me about 15 minutes for the first pass (`MaxDistanceSlow`) but all that looping put the solution at O(n^2). I knew that it could be faster. It took me another few hours to get `MaxDistance` which comes in at O(n-1). 
