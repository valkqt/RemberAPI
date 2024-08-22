using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(2)]
    public class TestMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Decks")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("IsPrivate").AsBoolean().NotNullable()
                .WithColumn("Scope").AsString(32).NotNullable();

            Create.ForeignKey("FK_DecksUsers")
                .FromTable("Decks").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_DecksUsers");
            Delete.Table("Decks");
        }
    }
}
