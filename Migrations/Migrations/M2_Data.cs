using FluentMigrator;

namespace Migrations.Migrations;

[Migration(2)]
public class M2_Data: Migration
{
    public override void Up()
    {
        Random rnd = new Random();
        var lorem = new Bogus.DataSets.Lorem(locale: "ru");
        
        Insert.IntoTable("Client").Row(new
        {
            Name = "Client1"
        });
        
        Insert.IntoTable("Client").Row(new
        {
            Name = "Client2"
        });
        
        Insert.IntoTable("CateringPoint").Row(new
        {
            Name = "CateringPoint1"
        });
        
        Insert.IntoTable("CateringPoint").Row(new
        {
            Name = "CateringPoint2"
        });

        for (int i = 0; i < 35; i++)
        {
            Insert.IntoTable("ClientToCateringPoint").Row(new
            {
                ClientId = rnd.Next(1, 3),
                CateringPointId = rnd.Next(1, 3),
                DayOfWeek = rnd.Next(1, 8),
                IsDeleted = rnd.Next(1, 1001) > 800,
                CreateDate = DateTime.Now.AddDays(-1 * rnd.Next(1, 8)),
                Action = lorem.Sentence(2)
            });
        }
    }

    public override void Down()
    {
    }
}