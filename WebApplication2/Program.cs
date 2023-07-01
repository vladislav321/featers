using Serilog;
using Serilog.Extensions.Hosting;
using WebApplication2;
using WebApplication2.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogExtension();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();
app.Run();

