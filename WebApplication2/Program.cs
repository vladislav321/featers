using Serilog;
using WebApplication2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var seqOptions = builder.Configuration.GetSection(nameof(SeqConfig)).Get<SeqConfig>();


var logger = new LoggerConfiguration()
    //.WriteTo.Console()
    .WriteTo.Seq(seqOptions.ServerUrl, apiKey: seqOptions.ApiKey)
    .ReadFrom.Configuration(builder.Configuration)
    //.Enrich.WithProperty("Application", seqOptions.ApiKey)
    //.Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    //.WriteTo.Seq("http://92.119.231.7:5341")
    .CreateLogger();

Log.Logger = logger;

builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
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

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

