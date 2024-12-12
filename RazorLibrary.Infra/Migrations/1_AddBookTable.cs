using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorLibrary.Infra.Migrations
{
    [Migration(1)]
    public class AddBookTable : Migration
    {
        public override void Up()
        {
            Create.Table("Books")
                .WithColumn("Id").AsGuid().PrimaryKey().Unique()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Publisher").AsString(70).NotNullable()
                .WithColumn("Photo").AsString(1000).NotNullable()
                .WithColumn("Authors").AsString(500).NotNullable();

        }

        public override void Down()
        {
            Delete.Table("Books");
        }
    }
}
