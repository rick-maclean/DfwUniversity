# DfwUniversity

This project was produced with the following tutorial

https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-5.0&tabs=visual-studio

If you are doing this with Visual Studio Code here are some useful links for development tools you need to install.

[Visual Studio Code] https://code.visualstudio.com/download

[C# for VS Code] https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

[.NET 5.0] https://dotnet.microsoft.com/download/dotnet/5.0

[SQLite download] https://www.sqlite.org/download.html

[DB Browser for SQLite] https://sqlitebrowser.org/

To create a new app with dotnet, here is a starting point. Note for this project DfwUniversity was used vs ContosoUniversity. Therefore when creatings namespaces DfwUniversity needs to be used if following the tutorial.
```bash
dotnet new webapp -o DfwUniversity
cd DfwUniversity  
```

Here are some of the commands that need to be run for Scaffolding pages
```bash
dotnet add package Microsoft.EntityFrameworkCore.SQLite -v 5.0.0-*
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.0-*
dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.0-*
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 5.0.0-*
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 5.0.0-*
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore -v 5.0.0-*  
```

install the aspnet-codegenerator scaffolding tool...

Windows
```bash
dotnet aspnet-codegenerator razorpage -m Student -dc DfwUniversity.Data.SchoolContext -udl -outDir Pages\\Students --referenceScriptLibraries -sqlite  
```

macOS of Linux
```bash
dotnet aspnet-codegenerator razorpage -m Student -dc Dfw University.Data.SchoolContext -udl -outDir Pages/Students --referenceScriptLibraries -sqlite  
```

generate the scaffolding for Students pages...(use the same pattern when needed for the other pages)
```bash
dotnet tool uninstall --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-aspnet-codegenerator --version 5.0.0-*  
```

This command will create the initial migration file to create the database
```bash
dotnet ef migrations add InitialCreate
```

This command will run the migration to create the database structure
```bash
dotnet ef database update
```

This command will build the code and will be used over and over as you modify the code
```bash
dotnet build
```

This command will run the application in the browser
```bash
dotnet run
```

