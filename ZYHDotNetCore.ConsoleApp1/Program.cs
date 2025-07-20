// See https://aka.ms/new-console-template for more information
using ZYHDotNetCore.Database.AppDbContextModels;

Console.WriteLine("Hello, World!");


AppDbContext appDbContext = new AppDbContext();

var list = appDbContext.TblBlogs.ToList();
foreach (var item in list)
{
    Console.WriteLine($"Title: {item.Title}, Author: {item.Author}, Content: {item.ContentData}");
    Console.WriteLine("--------------------------------------------------");
}