using API.Enteties;
using Microsoft.EntityFrameworkCore;

namespace MathApp.Enteties
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {

        }

        //private string _connectionString= "Server=desktop-2hi6019;Database=MathApp;User Id= DESKTOP-2HI6019\\poczt;Trusted_Connection=True; TrustServerCertificate=True";
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

//            modelBuilder.Entity<DefIncPair>().HasOne(v=>v.IncorrectDefinition).WithMany().HasForeignKey(v => v.IncorrectDefinitionId);       //.Property(r => r.IncorrectDefinitionId)

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  {
        //    optionsBuilder.UseSqlServer(_connectionString);

        // }
    }
}
