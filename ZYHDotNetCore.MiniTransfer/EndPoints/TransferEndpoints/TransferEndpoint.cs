using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.MiniTransfer.EndPoints.TransferEndpoints
{
    public static class TransferEndpoint
    {
        public static void MapTransferEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/transfer", (string fromPhNo, string toPhNo, int amount) =>
            {
                AppDbContext db = new AppDbContext();
                using var transaction = db.Database.BeginTransaction();
                var fromUser = db.TblUsers.FirstOrDefault(x => x.PhoneNumber.Equals(fromPhNo) && x.DeleteFlag == 0);
                var toUser = db.TblUsers.FirstOrDefault(x => x.PhoneNumber.Equals(toPhNo) && x.DeleteFlag == 0);
                if (fromUser is null || toUser is null)
                {
                    return Results.NotFound("One or both users not found");
                }
                else if (fromUser.Balance < amount)
                {
                    return Results.BadRequest("Insufficient balance");
                }
                else
                {
                    fromUser.Balance -= amount;
                    toUser.Balance += amount;
                    var result = db.SaveChanges();
                    if (result >= 2) // Check if both users were updated
                    {
                        db.TblTransactionHistories.Add(new TblTransactionHistory
                        {
                            FromNumber = fromPhNo,
                            ToNumber = toPhNo,
                            Type = "Transfer",
                            Amount = amount,
                            Date = DateTime.Now
                        });
                        var tranResult = db.SaveChanges();
                        if (tranResult > 0)
                        {
                            transaction.Commit();
                            return Results.Ok("Transfer Success");
                        }
                        else
                        {
                            transaction.Rollback();
                            return Results.Problem("Something went wrong while saving transaction history to the database.");
                        }
                    }
                    else
                    {
                        return Results.Problem("Something went wrong while saving to the database.");
                    }
                }
            })
            .WithName("Transfer")
            .WithTags("Transaction")
            .WithOpenApi();
        }
    }
}
