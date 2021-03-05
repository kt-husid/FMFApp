
Architecture

IMPORTANT:

Restoring SQL DB

If the DB Restore process is "stuck" in "restoring..." (with no recovery has probably been used), then run the following command after successful restoration of the DB:
"RESTORE DATABASE <database name> WITH RECOVERY" (without quotes)

EF 6 Update Database (run Code First Migrations on a remote SQL DB)
Command:
update-database -targetmigration:<NAME_OF_MIGRATION> -ConnectionStringName "<NAME_OF_CONNECTION_STRING_FOR_REMOTE_SQL_DB>"

example: 
update-database -ConnectionStringName "FMFWebAppContextRemote"


EF 6 has three ways of implementing inheritance (TPH, TPT & TPC) so it can be mapped to a relational DB. BUT!, neither of these three options seem fit (good enough).

Table per Hierarchy (TPH): This approach suggests one table for entire class inheritance hierarchy. Table includes discriminator column which distinguish between inheritance classes.
Table per Type (TPT): This approach suggests one table for each of the classes thus each class will have a persistence table.
Table per Concrete class (TPC): This approach suggests one table for one concrete class, but not for the abstract class. So if you inherit the abstract class in multiple concrete classes then the properties of the abstract class will be part of each table of concrete class.

I've decided to use composition over inheritance as it's easier to implement and change and best of all maps easily to a relational DB! Here's a good explanation:

"...But at that point, ask yourself whether it may not be better to remodel inheritance as delegation in the object model (delegation is a way of making composition 
	as powerful for reuse as inheritance). Complex inheritance is often best avoided for all sorts of reasons unrelated to persistence or ORM. EF acts as a buffer 
	between the domain and relational models, but that doesn’t mean you can ignore persistence concerns when designing your classes."
- Source : http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines

For more information regarding this pleas read:
* http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-inheritance-with-the-entity-framework-in-an-asp-net-mvc-application
* http://msdn.microsoft.com/en-us/data/jj591617
* http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines
* http://stackoverflow.com/questions/13540976/multiple-inheritance-with-entity-framework-tpc
* http://www.entityframeworktutorial.net/code-first/inheritance-strategy-in-code-first.aspx
* http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
* http://blogs.msdn.com/b/alexj/archive/2009/04/15/tip-12-choosing-an-inheritance-strategy.aspx


Conversion:
* Remove " (apostrophes) in all CSV
	* P100, PTXT
* LSLAG.Code set empty field to "uk" (unknown)
* P100.LAND should be corrected (GR > GL), etc.
* P100.SLAG which is empty > set "uk" (unknown)

Scaffolding:
 * use x86-platform to enable scaffolding if it fails

Building:
	> use x86-platform when:
		* Generating EF View Entity Data Model (read-only)
		* (running update-database on project BootstrapModel)
	> use x64-platform when:
		* Running DB Convert (use: reg add HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\11.0\WebProjects /v Use64BitIISExpress /t REG_DWORD /d 1)
		* Enable IIS 64 bit version (see image 'IIS64bitVersion.JPG')

http://stackoverflow.com/questions/19843683/vs2013-debugger-entity-framework-runtime-has-refused-to-evaluate-the-express


Internationalization
Part1: http://afana.me/post/aspnet-mvc-internationalization.aspx
Part2: http://afana.me/post/aspnet-mvc-internationalization-part-2.aspx

Front-End:
A JavaScript library for internationalization and localization that leverage the official Unicode CLDR JSON data. The library works both for the browser and as a Node.js module
https://github.com/jquery/globalize

National Language Support (NLS) API Reference
http://www.microsoft.com/resources/msdn/goglobal/default.mspx

Code First Migrations
http://msdn.microsoft.com/en-us/data/jj591621

Software Licences
http://choosealicense.com/licenses/

Phone Number Parser Library
https://code.google.com/p/libphonenumber/

Dashboard Sidebar Bootstrap Template
License & Author Details
MIT by Bootply.com
http://bootstrapzero.com/bootstrap-template/dashboard-sidebar

Underscore.js
https://github.com/jashkenas/underscore/blob/master/LICENSE

Web API
http://www.asp.net/web-api/overview/creating-web-apis/creating-api-help-pages

Bootstrap Datepicker
http://vitalets.github.io/bootstrap-datepicker/

Problems encountered
http://stackoverflow.com/questions/21918000/mvc5-vs2012-identity-createidentityasync-value-cannot-be-null

Knockout Kendo
http://rniemeyer.github.io/knockout-kendo/

jQuery Hotkeys
https://github.com/jeresig/jquery.hotkeys