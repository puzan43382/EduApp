using System.ComponentModel.DataAnnotations;

namespace EduAppAPI.Model
{
    public class AcademicProgram
    {
        [Key]
        public int ProgramId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
