using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduAppAPI.Model
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string? CourseCode { get; set; }
        public virtual ProgramCourseMapping CourseMapping { get; set; }

    }
}
