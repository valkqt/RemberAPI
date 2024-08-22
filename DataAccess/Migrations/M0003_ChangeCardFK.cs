using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations
{
    [Migration(3)]
    public class M0003_ChangeCardFK : Migration
    {
        public override void Up()
        {
            Create.Column("DeckId").OnTable("Cards").AsInt32().Nullable();


            Create.ForeignKey("FK_CardsDecks")
                .FromTable("Cards").ForeignColumn("DeckId")
                .ToTable("Decks").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_CardsDecks");
            Delete.Column("DeckId").FromTable("Cards");
        }
    }
}
