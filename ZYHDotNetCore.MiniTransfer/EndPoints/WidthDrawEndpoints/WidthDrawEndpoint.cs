using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.MiniTransfer.EndPoints.WidthDrawEndpoints
{
    public static class WidthDrawEndpoint
    {
        public static void MapWidthDrawEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/widthdraw", (string phNo, int amount) =>
            {
                AppDbContext db = new AppDbContext();
                var user = db.TblUsers.FirstOrDefault(x => x.PhoneNumber.Equals(phNo) && x.DeleteFlag == 0);
                if (user is null)
                {
                    return Results.NotFound("User not found");
                }
                else
                {
                    if (user.Balance < amount)
                    {
                        return Results.BadRequest("Insufficient balance");
                    }
                    var totalBal = user.Balance - amount;
                    user.Balance = totalBal;
                    var result = db.SaveChanges();
                    if (result is 1)
                    {
                        return Results.Ok("WidthDraw Success");
                    }
                    else
                    {
                        return Results.Problem("Something went wrong while saving to the database.");
                    }
                }
            })
            .WithName("WidthDraw")
            .WithTags("Transaction")
            .WithOpenApi();
        }
    }
}
