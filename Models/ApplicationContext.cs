using Microsoft.EntityFrameworkCore;

namespace TestDimaBack.Models
{
  public class ApplicationContext : DbContext
  {
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<CodeLanguage> CodeLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<EmployeeLanguage>()
          .HasKey(t => new { t.CodeLanguageId, t.EmployeeId });

      modelBuilder.Entity<EmployeeLanguage>()
          .HasOne(sc => sc.Employee)
          .WithMany(s => s.EmployeeLanguages)
          .HasForeignKey(sc => sc.EmployeeId);

      modelBuilder.Entity<EmployeeLanguage>()
          .HasOne(sc => sc.CodeLanguage)
          .WithMany(c => c.EmployeeLanguages)
          .HasForeignKey(sc => sc.CodeLanguageId);
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
     : base(options)
    {
      Database.EnsureCreated();
    }
  }
}