using EduAppAPI.Controllers;
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
    {}
    }
