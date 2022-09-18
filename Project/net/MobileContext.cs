using Project.net.Class;
using EntityFrameworkCore.Jet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace Project.net
{
    public class MobileContext : DbContext
    {
        public MobileContext() : base()
        {

        }
        public DbSet<Action> ActionsAll { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ButtonGen> ButtonGen { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Subdivision> Subdivision { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<StateTest> StateTest { get; set; }
        public DbSet<Normativ> Normativ { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet(Properties.Settings.Default.Connect);
            base.OnConfiguring(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateTest>().ToTable("StateTest");
            modelBuilder.Entity<Test>().ToTable("Test");
            modelBuilder.Entity<Normativ>().ToTable("Normativ");
        }
    }
}
