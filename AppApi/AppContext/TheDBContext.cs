using EduAppAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EduAppAPI.AppContext;

public partial class TheDBContext : DbContext
{
    public TheDBContext()
    {
         
    }

    public TheDBContext(DbContextOptions<TheDBContext> options, ILogger<TheDBContext> _logger)
       : base(options)
    {
        _logger = _logger;
    }

    public virtual DbSet<Course> Course { get; set; }

    public virtual DbSet<AcademicProgram> AcademicProgram { get; set; }

    public virtual DbSet<ProgramCourseMapping> ProgramCourseMapping { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.Entity<ProgramCourseMapping>(
       //     entity =>
       //     {
       //         entity.HasKey(pc => pc.Id);

       //         entity.Property(pc => pc.Id)
       //.ValueGeneratedOnAdd();


       //         modelBuilder.Entity<ProgramCourseMapping>()
       //     .Property(p => p.Id)
       //     .ValueGeneratedOnAdd();

       //         // UNIQUE CONSTRAINT (ProgramId + CourseId)
       //         entity.HasIndex(pc => new { pc.ProgramId, pc.CourseId })
       //               .IsUnique();

       //         // FOREIGN KEYS
       //         entity.HasOne(pc => pc.AcademicProgram)
       //               .WithMany()
       //               .HasForeignKey(pc => pc.ProgramId)
       //               .OnDelete(DeleteBehavior.Restrict);

       //         entity.HasOne(pc => pc.Course)
       //               .WithMany()
       //               .HasForeignKey(pc => pc.CourseId)
       //               .OnDelete(DeleteBehavior.Restrict);
       //     });
        }

    }
