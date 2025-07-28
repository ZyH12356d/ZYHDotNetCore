using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.MiniTransfer.EndPoints.WidthDrawEndpoints
{
    public static class DepositEndpoint
    {
        public static void MapDepositEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapPost("/api/deposit", (string phNo, int amount) =>
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
                    var totalBal = user.Balance + amount;
                    user.Balance = totalBal;
                    var result = db.SaveChanges();
                    if (result is 1)
                    {
                        db.TblTransactionHistories.Add(new TblTransactionHistory
                        {
                            FromNumber = phNo,
                            ToNumber = phNo, // Assuming deposit to self
                            Type = "Deposit",
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
                        return Results.Ok("Deposit Success");
                    }
                    else
                    {
                        return Results.Problem("Something went wrong while saving to the database.");
                    }
                }
            })
            .WithName("Deposit")
            .WithTags("Transaction")
            .WithOpenApi();
        }
    }
}
