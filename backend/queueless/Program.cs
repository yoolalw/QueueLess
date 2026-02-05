using Microsoft.EntityFrameworkCore;
using queueless.Data;
using queueless.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QueuelessContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UsersRoutes();
app.ProductsRoutes();
app.OrdersRoutes();

app.UseHttpsRedirection();
app.Run();