namespace WebApplication2.Migration;

using FluentMigrator;

[Migration(1)] // Уникальный номер миграции

public class EmployeeMigration : Migration
{
    public override void Up()
    {
        Create.Table("Employee")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("FirstName").AsString().NotNullable()
            .WithColumn("LastName").AsString().NotNullable()
            .WithColumn("Age").AsInt32().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Employee");
    }
}