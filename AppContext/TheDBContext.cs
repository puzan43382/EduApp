using EduAppAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EduAppAPI.AppContext;

public partial class TheDBContext : DbContext
{
    public TheDBContext()
    {
         
    }

    public TheDBContext(DbContextOptions<TheDBContext> options, ILogger<TheDBContext> logger)
       : base(options)
    {
        logger = logger;
    }

    public virtual DbSet<Course> Course { get; set; }

   
    
}
