# NPoco Identity Provider
Identity provider replacement for the default asp.net Entity Framework based provider

Loosely based on Microsoft's MySQLIdentity implementation. 

Requirements:
 * Microsoft Identity core (test on versions >= 2.2.0)
 * NPoco (tested on versions >= 2.8)
 * xUnit 2.1 or greater

If you restore the NugetPackages it will pull in:
 * Microsoft Identity core 2.2.1
 * NPoco 2.10.11 
 * xUnit 2.1

After those requirements are referenced in the projects you will be able to build. The current solution file is set to build against the .Net Framework 4.5.2.


### Running the unit tests
To run the tests update the connection string to point to your test database which has instance of the Identity schema (see below). 



### Installing the Identity Schema.
The NPoco Identity provider relies on the Microsoft Identity schema. When you start a new project in Visual Studio and use the WebApplication template with "Individual User Account" option, it will create a SQL Server compact edition file and update the connection string. When you create register a new user for the first time, the backing Entity Framework provider will initialize the table schema. 

__Option 1:__

The simplest way to grab the schema is to use the MVC/Webforms template, create an empty project and copy the MDF file located within "App_Data" directory. You can copy and rename the DB do your new NPoco Identity project.

__Option 2:__
You can use the SQL scripts located in the repo [Identity_SQL](https://github.com/pacal/Identity_SQL "Identity_SQL creation scripts"). Edit the variables at the top for the database name and filepath. Fire up SQL Management studio and run it against the server. It will create the database and all needed tables.


