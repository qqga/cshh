namespace cshh.Data.Services.Migrations
{
    using cshh.Data.Polyglot;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cshh.Data.Services.DbContexts.PolyglotDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(cshh.Data.Services.DbContexts.PolyglotDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Languages.AddOrUpdate(l => l.Name
            , new Language() { Name = "English", ShortName = "en" }
            , new Language() { Name = "Russian", ShortName = "ru" }
            , new Language() { Name = "Spanish", ShortName = "es" });

            context.WordTypes.AddOrUpdate(wt => wt.Name
            , new WordType() { Name = "���������������" }
            , new WordType() { Name = "��������������" }
            , new WordType() { Name = "������������" }
            , new WordType() { Name = "�����������" }
            , new WordType() { Name = "������" }
            , new WordType() { Name = "�������" }
            , new WordType() { Name = "�������" }
            , new WordType() { Name = "���������������" }
            , new WordType() { Name = "����" }
            , new WordType() { Name = "�������" }
            , new WordType() { Name = "����������" }
            , new WordType() { Name = "���������" }
            , new WordType() { Name = "����������" }
            , new WordType() { Name = "������������" });


            context.WordStatuses.AddOrUpdate(ws => ws.Name
            ,new WordStatus() { Name = "�����" }
            , new WordStatus() { Name = "���������" }
            );
        }
    }
}
