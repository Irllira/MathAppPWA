using Microsoft.EntityFrameworkCore;

namespace MathApp.Backend.Data.Enteties
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<EducationLevel> educationLevels { get; set; }

        public DbSet<IncorrectDefinition> IncorrectDefinitions { get; set; }

        public DbSet<DefIncPair> DefIncPair {  get; set; }

        public DbSet<Pages> Pages { get; set; }

        public DbSet<UserProgress> UserProgresses { get;set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(r => r.Username).IsRequired();
            modelBuilder.Entity<Account>().Property(r => r.Password).IsRequired();


        }
    }
}
