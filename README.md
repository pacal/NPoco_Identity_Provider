## Quick start:

* Download the [NPocoIdentity_WebApp_Template](https://github.com/pacal/NPocoIdentity_WebApp_Template) zip via the "Download Zip" button and unzip into a working directory
* If you want to see a custom implementation of the NPoco Identity User download from the [Implement_INPocoIdentity Branch](https://github.com/pacal/NPocoIdentity_WebApp_Template/tree/Implement_INPocoIdentity)
or

The templates will look very familiar to most people as it is based on the ASP.Net 4.52 MVC Template using the Individual User Account Authentication option

#### Background information:
[Identity Storage Providers](http://www.asp.net/identity/overview/extensibility/overview-of-custom-storage-providers-for-aspnet-identity)

The NPoco Identity Provider is loosely based on Microsoft's MYSql Identity Storage provider.

### The NPoco Identity Provider supplies the following User Role store which implements the following
* IUserLoginStore<TUser>
* IUserClaimStore<TUser>
* IUserRoleStore<TUser>
* IUserPasswordStore<TUser>
* IUserSecurityStampStore<TUser>
* IUserEmailStore<TUser>
* IUserPhoneNumberStore<TUser>
* IUserTwoFactorStore<TUser, string>
* IUserLockoutStore<TUser, string>
* IUserStore<TUser>

It does not implement IQueryable but does implement NPoco's [IQueryProviderWithIncludes](https://github.com/schotime/NPoco/wiki/Query-List) for Users which allows you to create simple linq like queries. 

While it of course allows you to set the Twofactor store values, you are responsible for implementing the framework which verifies everything, this is just allowing you to use NPoco as your ORM vs the EF provider.

```C#
            var iqUsers = UserStore.Users;
            var o = iqUsers.Where(x => x.UserName.EndsWith("3"));
            var threeList = o.ToList();
```
 
   
### Creating your own identity user:
 For quick and simple applications the included IdentityUser class will suffice. However if you wish to customize it, you will need to create a class that implements the INPocoIdentity<T> interface. You must also setup Tablename and Primary key attributes on the class for NPoco. The example below is taken from the [Implement_INPocoIdentity Branch](https://github.com/pacal/NPocoIdentity_WebApp_Template/tree/Implement_INPocoIdentity) template.

```C#
    [TableName("AspNetUsers")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class MyCustomUser : INPocoIdentity<MyCustomUser>
    {
        public MyCustomUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MyCustomUser(string userName)
            : this()
        {
            UserName = userName;
        }

        public string Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Comment { get; set; } // custom col
        public int Age { get; set; }        // custom col
        public string Title { get; set; }   // custom col

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyCustomUser> manager)
        {            
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
```
This example adds: Comment, Age, and Title to the custom Identity User. You will of course need to make sure you Database Schema reflects these new columns. 
As you can see we also have the NPoco attributes as well.

### Installing the Identity Schema
The NPoco Identity provider relies on the Microsoft Identity schema. When you start a new project in Visual Studio and use the WebApplication template with "Individual User Account" option, it will create a SQL Server compact edition file and update the connection string. When you create register a new user for the first time, the backing Entity Framework provider will initialize the table schema. 

Of course since we are ripping out EF, the above is not applicable. So you can use the templates listed above. Or manually create the Db.
You can use the SQL scripts located in the repo [Identity_SQL](https://github.com/pacal/Identity_SQL "Identity_SQL creation scripts"). Edit the variables at the top for the database name and filepath. Fire up SQL Management studio and run it against the server. It will create the database and all needed tables.
