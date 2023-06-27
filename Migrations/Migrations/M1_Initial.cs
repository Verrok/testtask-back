using FluentMigrator;

namespace Migrations.Migrations;

[Migration(1)]
public class M1_Initial: Migration
{
    public override void Up()
    {
        Create
            .Table("Client")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
            .WithColumn("Name").AsString().NotNullable();
        
        Create
            .Table("CateringPoint")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
            .WithColumn("Name").AsString().NotNullable();

        Create
            .Table("ClientToCateringPoint")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
            .WithColumn("ClientId").AsInt32().ForeignKey("Client", "Id").NotNullable()
            .WithColumn("CateringPointId").AsInt32().ForeignKey("CateringPoint", "Id").NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false)
            .WithColumn("DayOfWeek").AsInt32().NotNullable()
            .WithColumn("CreateDate").AsDateTime()
            .WithColumn("DeleteDate").AsDateTime().Nullable()
            .WithColumn("Action").AsString().NotNullable();
        
    }

    public override void Down()
    {
        Delete.Table("ClientToCateringPoint");
        Delete.Table("CateringPoint");
        Delete.Table("Client");
    }
}