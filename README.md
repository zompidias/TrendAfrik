## TRENDAFRIK PROJECT

This web project was created to aid buyers and sellers of African goods and services locate each other.  

TrendAfrik was done using ASPNet 2013, Microsoft Sql Server 2012, Bootstrap, MVC, C#.  

TABusinessLayer contains a C# library file for emails and other back end codes

The project will not run without an sql database. The sql script to recreate the Database, Tables and stored prcedures used. TrendAfrikscript.sql contains the script to recreate the database adn all the necessary objects


To rerun this project one will need to go into the webconfig page  and change the code part below to whatever suits one.   
 
> <connectionStrings>  
    <add name="TrendAfriqEntities" connectionString="server=JOSHUA\SQL2012; Initial Catalog=TrendAfriqOnlineCatalogue; Integrated Security=True; User ID=trendafriq; Password=trendafriq" providerName="System.Data.SqlClient"/>
    <add name="DBCS" connectionString="server=JOSHUA\SQL2012; Initial Catalog=TrendAfriqOnlineCatalogue; Integrated Security=True; User ID=trendafriq; Password=trendafriq" providerName="System.Data.SqlClient"/>  
  </connectionStrings>  

The screen shots for the projects have also been included.  

Pics TrendAfrik1 to TrendAfrik4 give an overview of the site.  

Trendafrik5 to Trendafrik9 show the control manager or control panel pages.   The site owner has the ability to add, delete or edit more names to the Catalogue links and to add,   delete or edit sellers details.   
