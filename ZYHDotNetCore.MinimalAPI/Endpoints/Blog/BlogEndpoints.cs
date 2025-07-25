using Microsoft.EntityFrameworkCore;
using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.MinimalAPI.Endpoints.Blog;

public static class BlogEndpoints
{
    public static void MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            AppDbContext dbContext = new AppDbContext();
            var lst = dbContext.TblBlogs.ToList();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();


        app.MapGet("/blogs/{id}", (int id) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                return Results.NotFound("No data");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            AppDbContext dbContext = new AppDbContext();
            dbContext.TblBlogs.Add(blog);
            var result = dbContext.SaveChanges();
            if (result > 0)
            {
                return Results.Ok(blog);
            }
            else
            {
                return Results.BadRequest("Failed to add blog.");
            }
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);

            if (item is null)
            {
                return Results.NotFound("Blog not found or already deleted.");
            }

            item.Title = blog.Title;
            item.Author = blog.Author;
            item.ContentData = blog.ContentData;
            dbContext.Entry(item).State = EntityState.Modified;
            //dbContext.TblBlogs.Update(item);
            dbContext.SaveChanges();
            return Results.Ok(item);

        })
        .WithName("EditBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var item = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                return Results.NotFound("Blog not found or already deleted.");
            }
            item.DeleteFlag = 1; // Mark as deleted
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
            return Results.Ok("Blog deleted successfully.");
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
