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

DapperExample dapperExample = new DapperExample();
dapperExample.Read();