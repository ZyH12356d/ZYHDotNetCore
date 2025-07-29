using Microsoft.AspNetCore.Http.HttpResults;
using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.MiniTransfer.EndPoints.UserRegisterEndpoints
{
    public static class UserRegisterEndpoint
    {
        public static void MapUserRegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/user", () =>
            {
                AppDbContext db = new AppDbContext();
                var lst = db.TblUsers.Where(x => x.DeleteFlag == 0).ToList();
                return Results.Ok(lst);
            })
            .WithName("UserRegisterList")
            .WithTags("User")
            .WithOpenApi();

            app.MapGet("/api/user/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var result = db.TblUsers.FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
                if(result is null)
                {
                    return Results.NotFound("User Not Found");
                }
                else
                {
                    return Results.Ok(result);
                }
            })
            .WithName("UserRegisterById")
            .WithTags("User")
            .WithOpenApi();
            app.MapGet("/api/user/balance/{PhNo}", (string PhNo) =>
            {
                AppDbContext db = new AppDbContext();
                var result = db.TblUsers.FirstOrDefault(x => x.PhoneNumber.Equals(PhNo) && x.DeleteFlag == 0);
                if(result is null)
                {
                    return Results.NotFound("User Not Found");
                }
                else
                {
                    return Results.Ok(result.Balance);
                }
            })
            .WithName("UserRegisterByPhNo")
            .WithTags("User")
            .WithOpenApi();

            app.MapPost("/api/user/register", (TblUser user) =>
            {
                AppDbContext db = new AppDbContext();
                db.TblUsers.Add(user);
                var result = db.SaveChanges();
                if (result is 1)
                {
                    return Results.Created($"/api/user/{user.Id}", user);
                }
                else
                {
                    return Results.BadRequest();
                }
            })
            .WithName("UserRegister")
            .WithTags("User")
            .WithOpenApi();
        }
    }
}
