using EduAppAPI.AppContext;
using EduAppAPI.Model;
using EduAppAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AcademicProgramController : ControllerBase
    {
        private readonly TheDBContext _dbContext;
        public AcademicProgramController(TheDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> AllAcademicProgram()
        {
            var AcademicPrograms = await _dbContext.AcademicProgram.ToListAsync();
            return Ok(AcademicPrograms);
        }
        [HttpGet]
        public async Task<IActionResult> GetProgramWithCourse(int id)
        {
            try
            {
                var result = await _dbContext.ProgramCourseMapping
                .Where(w => id == 0 || w.ProgramId == id)
                .GroupBy(g => new
                {
                    g.ProgramId,
                    ProgramName = g.AcademicProgram.Title
                })
                .Select(group => new
                {
                    ProgramID = group.Key.ProgramId,
                    ProgramName = group.Key.ProgramName,
                    Courses = group.Select(c => new
                    {
                        CourseID = c.CourseId,
                        CourseName = c.Course.Title
                    }).ToList()
                })
                .ToListAsync();


                //var program = await _dbContext.AcademicProgram
                //    //.Include(ap => ap.Course)
                //    .FirstOrDefaultAsync(ap => ap.ID == id);

                //if (program == null)
                //    return NotFound(new
                //    {
                //        message = $"Academic Program with ID {id} was not found"
                //    });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateAcademicProgram([FromBody] AcademicProgramCreateUpdateVModel postData)
        {
            if (string.IsNullOrWhiteSpace(postData.Name))
                return BadRequest("Name is required.");

            if (postData.Id > 0)
            {
                var existingData = await _dbContext.AcademicProgram.Where(cr => cr.ProgramId == postData.Id).FirstOrDefaultAsync();
                if (existingData != null)
                {
                    existingData.Title = postData.Name;
                    existingData.Description = postData.Description;
                    _dbContext.AcademicProgram.Update(existingData);
                    await _dbContext.SaveChangesAsync();
                    return Ok(existingData);
                }
                return BadRequest("No data found with the given ID");
            }
            var newAcademicProgram = new AcademicProgram
            {
                Title = postData.Name,
                Description = postData.Description
            };

            _dbContext.AcademicProgram.Add(newAcademicProgram);
            await _dbContext.SaveChangesAsync();

            return Ok(newAcademicProgram);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAcademicProgram(int id)
        {
            try
            {
                _dbContext.AcademicProgram.Where(cr => cr.ProgramId == id).ExecuteDelete();
                await _dbContext.SaveChangesAsync();
                return Ok("delete success");
            }
            catch (Exception ex)
            {
                return BadRequest("Internal Server Error");
            }
        }

    }
}
