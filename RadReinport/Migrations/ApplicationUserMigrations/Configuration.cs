namespace API.Migrations.ApplicationUserMigrations
{
    using API.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationUserMigrations";
            ContextKey = "API.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(u => u.UserName,
        new ApplicationUser
        {
            UserName = "CeterPasey",
            Email = "CeterPasey@mail.itsligo.ie",
            FirstName = "Peter",
            LastName = "Casey",
            PasswordHash = "Password$1",
        },
        new ApplicationUser
        {
            UserName = "RonnyJeed",
            Email = "RonnyJeed@mail.itsligo.ie",
            FirstName = "Jonny",
            LastName = "Reed",
            PasswordHash = "Password$2",
        },
        new ApplicationUser
        {
            UserName = "CoelNoneely",
            Email = "CoelNoneely@mail.itsligo.ie",
            FirstName = "Noel",
            LastName = "Conneely",
            PasswordHash = "Password$3",
        },
            new ApplicationUser
            {
                UserName = "NiallNulty",
                Email = "NiallNulty@mail.itsligo.ie",
                FirstName = "Niall",
                LastName = "Nulty",
                PasswordHash = "Password$4",
            }
        );
            context.SaveChanges();
        }
    }
}