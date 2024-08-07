using Proact.ActionFilter;
using Proact.Core.Value;
using Proact.MinimalApi;
using static Proact.Core.Tags;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddProact();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();


app.MapProact("/", c => div().With("Hello Word!!"));
app.MapProact("/Phil", () =>
{
    var v = DynamicValue.Create("counter", 0);
    return html().With(
        head(),
        body().With(
            div(onclick: v.Js.Set(c => c + 1))
                .With("Hello Phil!!!", v)));
});


app.Run();