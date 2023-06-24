using FluentMigrator.Runner;

namespace WebApplication2.Extension;

public static class MigrationExtension
{
    public static WebApplicationBuilder AddFluentMigration(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("PostgresConnection"))
                .ScanIn(typeof(Program).Assembly).For.Migrations());
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        return builder;
    }
    
    public static WebApplication UseFluentMigration(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
           var migrations = scope.ServiceProvider.GetService<IMigrationRunner>();
           migrations?.MigrateUp();
        }
        
        return app;
    }
}