using FluentMigrator;

namespace RazorLibrary.Infra.Migrations
{
    [Migration(2)]
    public class AlterSizeTitle : Migration
    {
        public override void Down()
        {
            Alter.Table("Books").AlterColumn("Title").AsString(0);
        }

        public override void Up()
        {
            Alter.Table("Books").AlterColumn("Title").AsString(70);
        }
    }
}
