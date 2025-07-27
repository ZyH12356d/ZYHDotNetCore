using Microsoft.EntityFrameworkCore;
using ZYHDotNetCore.Database.AppDbContextModels;
using ZYHDotNetCore.Domain.Features.Blog;

namespace ZYHDotNetCore.MinimalAPI.Endpoints.Blog;

public static class BlogServiceEndpoints
{
    public static void MapBlogServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            BlogServices blogService = new BlogServices();
            var lst = blogService.GetBlog();
            return Results.Ok(lst);
        })
        .WithName("GetBlogsservice")
        .WithOpenApi();


        app.MapGet("/blogs/{id}", (int id) =>
        {
            BlogServices blogService = new BlogServices();
            var item = blogService.GetBlogById(id);
            if (item is null)
            {
                return Results.NotFound("No data");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlogservice")
        .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            BlogServices blogService = new BlogServices();
            
            var result = blogService.CreateBlog(blog);
            if (result is not null)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.BadRequest("Failed to add blog.");
            }
        })
        .WithName("CreateBlogservice")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogServices blogService = new BlogServices();
            var item = blogService.EditBlog(id , blog);

            if (item is null)
            {
                return Results.NotFound("Blog not found or already deleted.");
            }

            
            return Results.Ok(item);

        })
        .WithName("EditBlogservice")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            BlogServices blogService = new BlogServices();
            var item = blogService.DeleteBlog(id);
            if (item is false)
            {
                return Results.NotFound("Blog not found or already deleted.");
            }
            
            return Results.Ok("Blog deleted successfully.");
        })
        .WithName("DeleteBlogservice")
        .WithOpenApi();
    }
}
