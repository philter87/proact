using System.Reflection.Metadata;
using Proact;

[assembly: MetadataUpdateHandler(typeof(HotReload))]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();
app.Run();
