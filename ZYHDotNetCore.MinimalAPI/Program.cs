using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZYHDotNetCore.Database.AppDbContextModels;
using ZYHDotNetCore.MinimalAPI.Endpoints.Blog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
//app.MapGet("/blogs", () =>
//{
//    AppDbContext dbContext = new AppDbContext();
//    var lst = dbContext.TblBlogs.ToList();
//    return Results.Ok(lst);
//})
//.WithName("GetBlogs")
//.WithOpenApi();


//app.MapGet("/blogs/{id}", (int id) =>
//{
//    AppDbContext dbContext = new AppDbContext();
//    var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
//    if(item is null)
//    {
//        return Results.NotFound("No data");
//    }
//    return Results.Ok(item);
//})
//.WithName("GetBlog")
//.WithOpenApi();

//app.MapPost("/blogs", (TblBlog blog) =>
//{
//    AppDbContext dbContext = new AppDbContext();
//    dbContext.TblBlogs.Add(blog);
//    var result = dbContext.SaveChanges();
//    if (result > 0)
//    {
//        return Results.Ok(blog);
//    }
//    else
//    {
//        return Results.BadRequest("Failed to add blog.");
//    }
//})
//.WithName("CreateBlog")
//.WithOpenApi();

//app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
//{
//    AppDbContext dbContext = new AppDbContext();
//    var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);

//    if (item is null)
//    {
//        return Results.NotFound("Blog not found or already deleted.");
//    }
    
//    item.Title = blog.Title;
//    item.Author = blog.Author;
//    item.ContentData = blog.ContentData;
//    dbContext.Entry(item).State = EntityState.Modified;
//    //dbContext.TblBlogs.Update(item);
//    dbContext.SaveChanges();
//    return Results.Ok(item);

//})
//.WithName("EditBlog")
//.WithOpenApi();

//app.MapDelete("/blogs/{id}", (int id) =>
//{
//    AppDbContext dbContext = new AppDbContext();
//    var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
//    if (item is null)
//    {
//        return Results.NotFound("Blog not found or already deleted.");
//    }
//    item.DeleteFlag = 1; // Mark as deleted
//    dbContext.Entry(item).State = EntityState.Modified;
//    dbContext.SaveChanges();
//    return Results.Ok("Blog deleted successfully.");
//})
//.WithName("DeleteBlog")
//.WithOpenApi();
//app.MapBlogEndpoints();

TblBlog blogtest = new TblBlog
{
    Id = 1,
    Title = "Sample Blog",
    Author = "Author Name",
    ContentData = "This is a sample blog content.",
    DeleteFlag = 0
};

string jsonStr = JsonConvert.SerializeObject(blogtest);
Console.WriteLine(jsonStr);
app.MapBlogServiceEndpoints();
app.Run();



//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
