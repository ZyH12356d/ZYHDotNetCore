using ZYHDotNetCore.MiniTransfer.EndPoints.TransferEndpoints;
using ZYHDotNetCore.MiniTransfer.EndPoints.UserRegisterEndpoints;
using ZYHDotNetCore.MiniTransfer.EndPoints.WidthDrawEndpoints;

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

app.MapUserRegisterEndpoints();
app.MapWidthDrawEndpoints();
app.MapTransferEndpoints();
app.MapDepositEndpoints();

app.Run();

