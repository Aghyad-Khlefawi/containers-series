using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
bool killed = false;
app.MapGet("/", () => killed ? Results.BadRequest("Service crashed") : Results.Ok("Hello World!"));

app.MapGet("/kill",()=>{
    killed = true;
    return "ok";
    });
app.Run();
