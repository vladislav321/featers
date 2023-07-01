using Serilog;

namespace WebApplication2.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilogExtension(this WebApplicationBuilder builder)
    {
        var seqOptions = builder.Configuration.GetSection(nameof(SeqConfig)).Get<SeqConfig>();
        var logger = new LoggerConfiguration()
            .WriteTo.Seq(seqOptions.ServerUrl, apiKey: seqOptions.ApiKey)
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithProperty("Application", seqOptions.ApiKey)
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .CreateLogger();

        Log.Logger = logger;

        builder.Logging.AddSerilog(logger);
        builder.Host.UseSerilog(logger);

        return builder;
    }
}