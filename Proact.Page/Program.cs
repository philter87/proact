using Proact.ActionFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// AddProact
builder.Services.AddProact();

var app = builder.Build();

// UseProact
app.UseProact();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();