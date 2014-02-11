The demo is broken up into a series of sections that showcase a specific features. Each section has a corresponding slide in the slide deck, which you can use to provide a quick summary of the feature before demoing it. This also helps break up the long demo section of the talk into logical 5-10min chunks, which helps folks keep up to speed.

### One Time Setup
* You'll need a machine with Visual Studio 2013 installed
* Clone this Git repo to your local machine
* Register the code snippets in Visual Studio
 * Open VS -> Tools -> Code Snippets Manager...
 * Make sure **Visual C#** is selected in the dropdown and add the **CodeSnippets** directory from your local clone of this repository
* Open **QuoteOfTheDay\QuoteOfTheDay.sln** and build the solution (this will create the **QuoteOfTheDay.1.0.0.0.nupkg** NuGet package in the **QuoteOfTheDay\bin\debug** folder)
* [Setup a local NuGet feed on your machine](http://docs.nuget.org/docs/creating-packages/hosting-your-own-nuget-feeds) and copy in the **QuoteOfTheDay.1.0.0.0.nupkg** that you created in the previous step
* To insulate yourself against network outages, slow WiFi, etc. I would also recommend copying all the NuGet packages needed to complete the talk to your local feed (I usually disable everything but my local feed - nothing worse than getting long pauses due to a slow network). The easiest way to get a copy of the required .nupkg files is to complete this demo once online then go to the packages folder of the completed solution and you'll find the .nupkg's nested under the sub-directory for each package. You can also log into [nugget.org](http://nugget.org) to get a 'Download' link on the page for each package (not shown unless you login), or use [NuGet Package Explorer](http://apps.microsoft.com/windows/en-us/app/3148c5ae-7307-454b-9eca-359fff93ea19). The total list of packages that you need to complete the talk is:
  * EntityFramework (anything greater than 6.0.0 will work fine for this demo)
  * NLog
  * NLog.Config
  * NLog.Schema
  * QuoteOfTheDay (from the previous step)

### Every Time Setup
* Run Visual Studio as an administrator (running as an administrator seems to minimize occurrences of the issue mentioned in the next point)
* Run **Get-ExecutionPolicy** in Package Manager Console (PMC) and ensure it returns **RemoteSigned**
 * Occasionally PMC sets the **Restricted** execution policy and won't allow running install scripts from the NuGet packages. It's really hard to recover from… **don't skip this step!**
* Connect to **(localdb)\v11.0** in SQL Server Object Explorer (if you have a SKU of Visual Studio which includes it). If you have a version of VS without it, you should grab [SQL Server Management Studio](http://www.microsoft.com/en-us/download/details.aspx?id=29062) instead.
  * I recommend dropping all databases from LocalDb before the demo - it's just less noise for folks to process.

### Demo 1: Basic Code First Functionality
* Create a new Console Application

#### Intro to Code First
* Install the **EntityFramework** NuGet package
* Add a new **Model.cs** class file
 * Replace the Model class (just the class definition, leave the namespace and usings in place) with a pre-built model | **Code Snippet: HereIsOneWePreparedEarlier**
 * Resolve namespace for data annotations (Tip: You can click on one of the [Key] annotations and press **Ctrl+.** or just right-click on it and select **Resolve** from the context menu)
* Add a new **BloggingContext.cs** class file
 * Add **DbContext** as the base class (resolve namespace for DbContext)
 * Add DbSets to the class | **Code Snippet: AddADbSetForEachTypeInMyModel**
* Add the CreateAndPrintBlogs worker method to Program.cs | **Code Snippet: DoSomeDatabaseStuff**
 * Add a call to **CreateAndPrintBlogs** in the **Main** method
```
static void Main(string[] args)
{
    CreateAndPrintBlogs();
}
```
* Run application
 * Tip: You can use **Ctrl+F5** and VS will launch with an automatic 'press any key to exit' message
 * Show that the database was created on localdb\v11.0 (show how Code First calculated schema by convention)

#### Intro to Migrations
* Run **Enable-Migrations** in Package Manager Console
 * Show that an InitialCreate migration that was scaffolded to represent schema already created in the database
* Add a **Rating** (int) property to the **Blog** class
 * Tip: You can use the built-in **prop** code snippet to do this 
```
public int Rating { get; set; }
```
* Run **Add-Migration BlogRating** in Package Manager Console
 * Edit the scaffolded code to specify a defaultValue of 3. The key point here is that this is just scaffolded code (EF's best guess at what you want to do) and you now own it and can edit as you see fit.
```
AddColumn("dbo.Blogs", "Rating", c => c.Int(nullable: false, defaultValue: 3));
```
* Run **Update-Database** in Package Manager Console
* Show database is updated with new column
 * Show __MigrationHistory table and explain that it contains a list of migrations that have been applied to the database

### Demo 2: Conventions

#### Key Convention
* Remove **[Key]** attributes from **Model.cs** (4 attributes total)
* Run app and receive an exception about keys
* Override **OnModelCreating** in **BloggingContext.cs**
 * Tip: Inside the BloggingContext class, type **override** then press space, select **OnModelCreating** from the list, and press **Tab**
* In **OnModelCreating** add in a convention to detect properties named **Key** as a primary key | **Backup Code Snippet: KeyConvention**
```
modelBuilder.Properties()
    .Where(p => p.Name ==  "Key")
    .Configure(p => p.IsKey());
```
* Run the app and show that everything now works
	
#### Column Name Conventions
* Add a GetColumnName helper method to BloggingContext | **Code Snippet: GetColumnName**
 * Resolve namespaces for PropertyInfo & Regex
* Add a convention to use this to set column names
```
modelBuilder
    .Properties()
    .Configure(p => p.HasColumnName(GetColumnName(p.ClrPropertyInfo)));
```
* Run **Add-Migration ColumnNaming** in Package Manager Console
* Run **Update-Database** in Package Manager Console
* Show database with the new column names

#### Data Type Convention
* Show that strings default to **nvarchar(max)** in the database
* Add a convention to set string properties to have a max length of 200
```
modelBuilder
    .Properties<string>()
    .Configure(p => p.HasMaxLength(200));
```
* Run **Add-Migration ShortStrings** in Package Manager Console
* Run **Update-Database** in Package Manager Console
 * Show that strings are now **nvarchar(200)** by default
	
### Demo 3: Database Logging
* In Program.cs, in CreateAndPrintBlogs() just after the context is constructed, wire up **Log** to the console.
```
static void CreateAndPrintBlogs()
{
    using (var db = new BloggingContext())
    {
        db.Database.Log = Console.Write;
...
```
* Run and show output

### Demo 4: Code First Stored Procedures ###

##### Basic SPROC Mapping ####
* In BlogContext.cs add code in OnModelCreating to map Blog to a stored procedure
```
modelBuilder
    .Entity<Blog>()
    .MapToStoredProcedures();
```
* Run **Add-Migration BlogSPROCs** in Package Manager Console
* Run **Update-Database** in Package Manager Console
* Run app and show that logging output (from previous demo) shows EF using SPROCs

#### Custom SPROC Mapping & Customized SPROC Bodies ####
* Modify code in OnModelCreating to tweak the shape of the insert stored procedures for Blog
```
modelBuilder
    .Entity<Blog>()
    .MapToStoredProcedures(s =>
        s.Insert(i => i.HasName("CreateBlog").Parameter(b => b.Rating, "blog_score")));
```
* Run **Add-Migration TweakSPROC** in Package Manager Console
 * Edit the scaffolded code to create the BlogCreationLog table before any of the scaffolded operations (i.e. first line inside the **Up** method) | **Code Snippet: BlogCreationLog**
 * Some SQL to write to this table is included (commented out) in the code snippet. Move this SQL to be the second last block in the body of the stored procedure (i.e. just before the final SELECT statement).
* Run **Update-Database** in Package Manager Console
* Run the app 
 * Open the **BlogCreationLog** table to show that it was written to
	
#### SPROCs and Custom Conventions ####
* In OnModelCreating add a convention to map all entities to SPROCs 
```
modelBuilder  
    .Types()  
    .Configure(t => t.MapToStoredProcedures());
```
* Run **Add-Migration AllSPROCs** in Package Manager Console
* Run **Update-Database** in Package Manager Console
* Show database with all the SPROCs 
	
### Demo 5: Async ####
* Install the **QuoteOfTheDay** NuGet package
* In Program.cs, in the Main method after the call to CreateAndPrintBlogs() add code to print a quote 
```
static void Main(string[] args)
{
    CreateAndPrintBlogs();

    Console.WriteLine("Quote: " + QuoteOfTheDay.Quotes.GetQuote());
}
```
* Run the app and show synchronous output (i.e. insert for new blog, query initiated, query results are displayed, then quote is displayed)
* Add a using for **System.Data.Entity** to get the async LINQ extension methods
* Change CreateAndPrintBlogs to 'async Task'
```
static async Task CreateAndPrintBlogs()
{
    ...
}
```
 * Update SaveChanges call to be async
```
...
await db.SaveChangesAsync();
...
```
 * Update query to be async
```
...
var blogs = await (from b in db.Blogs
                orderby b.Name
                select b).ToListAsync();
...
```
* Update Main to wait on the returned task at end of execution
```
static void Main(string[] args)
{
    var task = CreateAndPrintBlogs();

    Console.WriteLine("Quote: " + QuoteOfTheDay.Quotes.GetQuote());

    task.Wait();
}
```
* Run and show async output(i.e. quote is written out somewhere in between database operations)

### Demo 6: Code Based Configuration ###

* Add a **MyConfig** class
 * Update it to derive from **DbConfiguration**
 * Resolve namespace for DbConfiguration
 * Add a constructor (Tip: You can use the built-in **ctor** code snippet)

#### Pluralization Service ####
* Show that the table name for **Status** is pluralized incorrectly in the database (EF thinks the plural is **Status** but it technically should be **Statuses**)
* Add pluralization service (either in a new file, or below the MyConfig class) | **Code Snippet: MoreBetterEnglishPluralizationService**
 * Resolve namespace for IPluralizationService
* Register the new service on the constructor of MyConfig
```
public MyConfig()
{
    SetPluralizationService(new MoreBetterEnglishPluralizationService());
}
```
* Run **Add-Migration BetterPlurals** in Package Manager Console
* Run **Update-Database** in Package Manager Console
* Show that table name is updated in database
	
#### Interceptors ####
* Install the **NLog.Config** NuGet package
* Open the **NLog.config** file that was added to your project and uncomment the default settings
  * Tip: You can highlight the lines and type **Ctrl+k Ctrl+u**
```
<target xsi:type="File" name="f" fileName="${basedir}/logs/${shortdate}.log"
    layout="${longdate} ${uppercase:${level}} ${message}" />
```
```
<logger name="*" minlevel="Trace" writeTo="f" />
```
* Add an NLog interceptor (either in a new file, or below the MyConfig class) | **Code Snippet: NLogInterceptor**
 * Resolve namespaces for IDbCommandInterceptor and DbCommand
* Register the new interceptor on the constructor of MyConfig
```
public MyConfig()
{
    SetPluralizationService(new MoreBetterEnglishPluralizationService());
    AddInterceptor(new NLogInterceptor());
}
```
 * Run the application and open the **bin\debug\logs** folder on disk. Open the single log file and show the contents.