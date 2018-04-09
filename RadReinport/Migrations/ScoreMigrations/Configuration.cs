namespace API.Migrations.ScoreMigrations
{
    using API.Models.Scores;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<API.Models.Scores.ScoresDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ScoreMigrations";
        }

        protected override void Seed(ScoresDbContext context)
        {
            context.Scores.AddOrUpdate(s => s.Username,
                new Score
                {
                    UserName = "CeterPasey",
                    HighScore = 10
                },
                new Score
                {
                    UserName = "RonnyJeed",
                    HighScore = 20
                },
                new Score
                {
                    UserName = "CoelNoneely",
                    HighScore = 30
                },
                new Score
                {
                    UserName = "NiallNulty",
                    HighScore = 40
                }
                );
            context.SaveChanges();
        }
    }
}