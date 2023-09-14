using Proact.ActionFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProact();

var app = builder.Build();
app.UseProact();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();