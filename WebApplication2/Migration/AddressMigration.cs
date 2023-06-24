namespace WebApplication2.Migration;

using FluentMigrator;

[Migration(2)] // Уникальный номер миграции

public class AddressMigration  : Migration
{
    public override void Up()
    {
        Create.Table("Address")
            .WithColumn("Id").AsInt64().NotNullable().Identity().PrimaryKey()
            .WithColumn("Street").AsString().NotNullable()
            .WithColumn("City").AsString().NotNullable()
            .WithColumn("State").AsString().NotNullable()
            .WithColumn("Zip").AsString().Nullable()
            .WithColumn("EmployeeId").AsInt32().NotNullable().ForeignKey("Employee", "Id");
 
        Create.Index("IX_Address_EmployeeId")
            .OnTable("Address")
            .OnColumn("EmployeeId")
            .Ascending()
            .WithOptions().NonClustered();
    }

    public override void Down()
    {
        Delete.Index("IX_Address_EmployeeId").OnTable("Address");
        Delete.Table("Address");    }
}