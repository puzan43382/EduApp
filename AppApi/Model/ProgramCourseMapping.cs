using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduAppAPI.Model
{
    public class ProgramCourseMapping
    {
        [Key]
        public int ID { get; set; }
        public int ProgramId { get; set; }
        public int CourseId { get; set; }
        // Mark navigation properties as nullable to avoid this error
        [ForeignKey(nameof(ProgramId))]
        public virtual AcademicProgram AcademicProgram { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
    }
}
