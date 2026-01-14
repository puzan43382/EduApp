using EduAppAPI.AppContext;
using EduAppAPI.DTOs;
using EduAppAPI.Model;
using EduAppAPI.ViewModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProgramCourseController : ControllerBase
    {
        private readonly TheDBContext _dbcontext;

        public ProgramCourseController(TheDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProgramCourse()
        {
            var programcourse = await _dbcontext.ProgramCourseMapping.ToListAsync();
            return Ok(programcourse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateProgramCourseMapping([FromBody] ProgramCourseDTO dto)
        {
            try
            {
                if (dto.Id > 0)
                {
                    var existingData = await _dbcontext.ProgramCourseMapping
                        .FirstOrDefaultAsync(pc => pc.ID == dto.Id);

                    if (existingData != null)
                    {
                        existingData.ProgramId = dto.ProgramId;
                        existingData.CourseId = dto.CourseId;

                        _dbcontext.ProgramCourseMapping.Update(existingData);
                        await _dbcontext.SaveChangesAsync();

                        return Ok(existingData);
                    }

                    return BadRequest("No data found with the given ID");
                }
                var alreadyExists = await _dbcontext.ProgramCourseMapping
                    .AnyAsync(pc => pc.ProgramId == dto.ProgramId &&
                                    pc.CourseId == dto.CourseId);

                if (alreadyExists)
                    return BadRequest("This Id already exists.");

                var newMapping = new ProgramCourseMapping
                {
                    ProgramId = dto.ProgramId,
                    CourseId = dto.CourseId
                };

                _dbcontext.ProgramCourseMapping.Add(newMapping);
                await _dbcontext.SaveChangesAsync();

                return Ok(newMapping);
            }
            catch (Exception)
            {
                return BadRequest("Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesByProgramId(int ProgramId)
        {
            try
            {

                //var programName = await _dbcontext.ProgramCourseMapping
                //    .Where(pc => pc.ProgramId == ProgramId)
                //    .Select(pc => pc.AcademicProgram.Title)
                //    .FirstOrDefaultAsync();

                //if (programName == null)
                //{
                //    return NotFound(new
                //    {
                //        message = $"Program with ID {ProgramId} was not found"
                //    });
                //}


                //var courses = await _dbcontext.ProgramCourseMapping
                //    .Where(pc => pc.ProgramId == ProgramId)
                //    .Join(
                //        _dbcontext.Course,
                //        pcm => pcm.CourseId,
                //        c => c.CourseId,
                //        (pcm, c) => new
                //        {
                //            CourseId = c.CourseId,
                //            CourseName = c.Title
                //        })
                //    .ToListAsync();

                //// 3️⃣ Final response
                //var result = new
                //{
                //    ProgramId = ProgramId,
                //    ProgramName = programName,
                //    Courses = courses
                //};

                //return Ok(result);

                var result = await _dbcontext.ProgramCourseMapping
                .Where(w => ProgramId == 0 || w.ProgramId == ProgramId)
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
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMapping(int ProgramCourseMappingId)
        {
            try
            {
                var mapping = await _dbcontext.ProgramCourseMapping
                    .FirstOrDefaultAsync(pc => pc.ID == ProgramCourseMappingId);

                if (mapping == null)
                    return NotFound("Mapping not found");

                _dbcontext.ProgramCourseMapping.Remove(mapping);
                _dbcontext.SaveChanges();

                return Ok(mapping);
            }
            catch (Exception)
            {
                return BadRequest("Internal Server Error");
            }
        }
    }
}