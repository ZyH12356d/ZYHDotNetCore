// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZYHDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");
//Console.ReadKey();
//Console.ReadLine();

// To Check server name in sql server = select @@SERVERNAME 
//Data source = server name | Initial Catalog = database name


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.read();
//adoDotNetExample.Delete();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("Dapper Example", "Zahid", "This is a Dapper example");
//dapperExample.Update(1, "Updated Title", "Updated Author", "Updated Content");
//dapperExample.Delete(2);

//EfCoreExample ef = new EfCoreExample();
//ef.Read();
//ef.Create("EfBlogTitle" , "EfBlogAuthor" , "EfBlogContent");
//ef.GetBlogById(1);
//ef.GetBlogById(5);
//ef.Update(5, "Updated Ef Title", "Updated Ef Author", "Updated Ef Content");
//ef.Delete(5);

AdoExample ado = new AdoExample();
ado.Read();
//ado.Create();

// AsNoTracking
// Select * from Tbl_Blog with (nolock); in mysql and mssql select both commit and uncommit data.
// AsNoTracking is used to read data without tracking changes in the context.
// if in blog table there is a row that is update or something this will wait for the commit to be done before reading the data.
// commit data / uncommit data
//