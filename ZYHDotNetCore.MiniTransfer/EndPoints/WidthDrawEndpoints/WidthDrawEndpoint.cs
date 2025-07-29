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
                using var transaction = db.Database.BeginTransaction();
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
                        db.TblTransactionHistories.Add(new TblTransactionHistory
                        {
                            FromNumber = phNo,
                            ToNumber = "Bnank WidthDraw", // Assuming deposit to self
                            Type = "WidthDraw",
                            Amount = amount,
                            Date = DateTime.Now
                        });
                        var tranResult = db.SaveChanges();
                        if (tranResult is 0)
                        {
                            transaction.Rollback();
                            return Results.Problem("Something went wrong while saving transaction history to the database.");
                        }
                        transaction.Commit();
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
