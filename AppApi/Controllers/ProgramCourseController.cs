using EduAppAPI.AppContext;
using EduAppAPI.DTOs;
using EduAppAPI.Model;
using EduAppAPI.ViewModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [HttpPost]
        public async Task<IActionResult> AddProgramWithMultipleCourses(
          [FromBody] List<ProgramCourseDTO> dtoList)
        {
            if (dtoList == null || !dtoList.Any())
                return BadRequest("No data provided.");

            //int totalProgramsUpdated = 0;

            foreach (var dto in dtoList)
            {
                bool programExists = await _dbcontext.AcademicProgram
                    .AnyAsync(p => p.ProgramId == dto.ProgramId);

                if (!programExists)
                    return BadRequest($"Program does not exist: {dto.ProgramId}");

                if (dto.CourseId == null || !dto.CourseId.Any())
                    return BadRequest($"No CourseIds provided for ProgramId {dto.ProgramId}");

                var activeCourseIds = dto.CourseId
                .Where(c => c.IsActive == true)
                .Select(c => c.Id)
                .Distinct()
                .ToList();

                if (!activeCourseIds.Any())
                {
                    var existing = await _dbcontext.ProgramCourseMapping
                        .Where(pc => pc.ProgramId == dto.ProgramId)
                        .ToListAsync();

                    _dbcontext.ProgramCourseMapping.RemoveRange(existing);
                   // totalProgramsUpdated++;
                    continue;
                }
                var validCourseIds = await _dbcontext.Course
                .Where(c => activeCourseIds.Contains(c.CourseId))
                .Select(c => c.CourseId)
                .ToListAsync();

                var invalidCourseIds = activeCourseIds.Except(validCourseIds).ToList();
                if (invalidCourseIds.Any())
                    return BadRequest(
                        $"Invalid CourseIds for ProgramId {dto.ProgramId}: {string.Join(", ", invalidCourseIds)}");

            var existingMappings = await _dbcontext.ProgramCourseMapping
                .Where(pc => pc.ProgramId == dto.ProgramId)
                .ToListAsync();

            _dbcontext.ProgramCourseMapping.RemoveRange(existingMappings);


            var newMappings = activeCourseIds.Select(courseId =>
                new ProgramCourseMapping
                {
                    ProgramId = dto.ProgramId,
                    CourseId = courseId
                }).ToList();

            _dbcontext.ProgramCourseMapping.AddRange(newMappings);

            //totalProgramsUpdated++;
        }

        await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
               // ProgramsUpdated = totalProgramsUpdated,
                Message = "Program-course mappings replaced successfully"
            });
        }
    }
}








//using EduAppAPI.AppContext;
//using EduAppAPI.DTOs;
//using EduAppAPI.Model;
//using EduAppAPI.ViewModel;
//using Microsoft.AspNetCore.Hosting.Server;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace EduAppAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]/[action]")]
//    public class ProgramCourseController : ControllerBase
//    {
//        private readonly TheDBContext _dbcontext;

//        public ProgramCourseController(TheDBContext dbcontext)
//        {
//            _dbcontext = dbcontext;
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllProgramCourse()
//        {
//            var programcourse = await _dbcontext.ProgramCourseMapping.ToListAsync();
//            return Ok(programcourse);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateUpdateProgramCourseMapping([FromBody] ProgramCourseDTO dto)
//        {
//            try
//            {
//                if (dto.Id > 0)
//                {
//                    var existingData = await _dbcontext.ProgramCourseMapping
//                        .FirstOrDefaultAsync(pc => pc.ID == dto.Id);

//                    if (existingData != null)
//                    {
//                        existingData.ProgramId = dto.ProgramId;
//                        existingData.CourseId = dto.CourseId;

//                        _dbcontext.ProgramCourseMapping.Update(existingData);
//                        await _dbcontext.SaveChangesAsync();

//                        return Ok(existingData);
//                    }

//                    return BadRequest("No data found with the given ID");
//                }
//                var alreadyExists = await _dbcontext.ProgramCourseMapping
//                    .AnyAsync(pc => pc.ProgramId == dto.ProgramId &&
//                                    pc.CourseId == dto.CourseId);

//                if (alreadyExists)
//                    return BadRequest("This Id already exists.");

//                var newMapping = new ProgramCourseMapping
//                {
//                    ProgramId = dto.ProgramId,
//                    CourseId = dto.CourseId
//                };

//                _dbcontext.ProgramCourseMapping.Add(newMapping);
//                await _dbcontext.SaveChangesAsync();

//                return Ok(newMapping);
//            }
//            catch (Exception)
//            {
//                return BadRequest("Internal Server Error");
//            }
//        }



//        [HttpGet]
//        public async Task<IActionResult> GetCoursesByProgramId(
//            int ProgramId,
//            int page = 1,
//            int pageSize = 2)
//        {
//            try
//            {
//                if (page < 1) page = 1;
//                if (pageSize < 1) pageSize = 2;

//                var groupQuery = _dbcontext.ProgramCourseMapping
//                .Where(w => ProgramId == 0 || w.ProgramId == ProgramId)
//                .GroupBy(g => new
//                {
//                    g.ProgramId,
//                    ProgramName = g.AcademicProgram.Title
//                })
//                .Select(group => new
//                {
//                    ProgramID = group.Key.ProgramId,
//                    ProgramName = group.Key.ProgramName,
//                    Courses = group.Select(c => new
//                    {
//                        CourseID = c.CourseId,
//                        CourseName = c.Course.Title
//                    }).ToList()
//                });
//                var totalPrograms = await groupQuery.CountAsync();

//                var pagedResult = await groupQuery
//                   .OrderBy(g => g.ProgramID)
//                   .Skip((page - 1) * pageSize)
//                   .Take(pageSize)
//                   .ToListAsync();
//                return Ok(new
//                {
//                    page,
//                    pageSize,
//                    totalPrograms,
//                    totalPages = (int)Math.Ceiling((double)totalPrograms / pageSize),
//                    data = pagedResult
//                });
//            }

//            catch (Exception)
//            {
//                return StatusCode(500, "Internal Server Error");
//            }
//        }


//        [HttpDelete]
//        public async Task<IActionResult> DeleteMapping(int ProgramCourseMappingId)
//        {
//            try
//            {
//                var mapping = await _dbcontext.ProgramCourseMapping
//                    .FirstOrDefaultAsync(pc => pc.ID == ProgramCourseMappingId);

//                if (mapping == null)
//                    return NotFound("Mapping not found");

//                _dbcontext.ProgramCourseMapping.Remove(mapping);
//                _dbcontext.SaveChanges();

//                return Ok(mapping);
//            }
//            catch (Exception)
//            {
//                return BadRequest("Internal Server Error");
//            }
//        }
//    }
//}


//[HttpGet]
//public async Task<IActionResult> GetCoursesByProgramId(int ProgramId)
//{
//    try
//    {

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