using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor) { }

        [HttpDelete]
        [Route("Delete/{StudentID}/{SectionID}/{GradeTypeCode}/{SchoolID}/{GradeCodeOccurence}")]
        public async Task<IActionResult> Delete(int StudentID, int SectionID, string GradeTypeCode, int SchoolID, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.StudentId == StudentID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        public Task<IActionResult> Delete(int KeyVal)
        {//Needed for baseController
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Grades.Select(sp => new GradeDTO
                {
                    Student_id = sp.StudentId,
                    Section_id = sp.SectionId,
                    School_id = sp.SchoolId,
                    Grade_type_code = sp.GradeTypeCode,
                    Grade_code_occurrence = sp.GradeCodeOccurrence,
                    Numeric_grade = sp.NumericGrade,
                    Comments = sp.Comments
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        [HttpGet]
        [Route("Get/{StudentID}/{SectionID}/{GradeTypeCode}/{SchoolID}/{GradeCodeOccurence}")]
        public async Task<IActionResult> Get(int StudentID, int SectionID, string GradeTypeCode, int SchoolID, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.StudentId == StudentID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                     .Select(sp => new GradeDTO
                     {
                         Student_id = sp.StudentId,
                         Section_id = sp.SectionId,
                         School_id = sp.SchoolId,
                         Grade_type_code = sp.GradeTypeCode,
                         Grade_code_occurrence = sp.GradeCodeOccurrence,
                         Numeric_grade = sp.NumericGrade,
                         Comments = sp.Comments
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.StudentId == _GradeDTO.Student_id)
                .Where(x => x.SectionId == _GradeDTO.Section_id)
                    .Where(x => x.SectionId == _GradeDTO.Section_id)
                    .Where(x => x.GradeTypeCode == _GradeDTO.Grade_type_code)
                    .Where(x => x.SchoolId == _GradeDTO.School_id)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.Grade_code_occurrence).FirstOrDefaultAsync();
                if (itm == null)
                {
                    Grade e = new Grade
                    {
                        StudentId = _GradeDTO.Student_id,
                        SectionId = _GradeDTO.Section_id,
                        SchoolId = _GradeDTO.School_id,
                        GradeTypeCode = _GradeDTO.Grade_type_code,
                        GradeCodeOccurrence = (byte)_GradeDTO.Grade_code_occurrence,
                        NumericGrade = _GradeDTO.Numeric_grade,
                        Comments = _GradeDTO.Comments
                    };
                    _context.Grades.Add(e);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.StudentId == _GradeDTO.Student_id)
                    .Where(x => x.SectionId == _GradeDTO.Section_id)
                    .Where(x => x.GradeTypeCode == _GradeDTO.Grade_type_code)
                    .Where(x => x.SchoolId == _GradeDTO.School_id)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.Grade_code_occurrence).FirstOrDefaultAsync();

                itm.StudentId = _GradeDTO.Student_id;
                itm.SectionId = _GradeDTO.Section_id;
                itm.SchoolId = _GradeDTO.School_id;
                itm.GradeTypeCode = _GradeDTO.Grade_type_code;
                itm.GradeCodeOccurrence = (byte)_GradeDTO.Grade_code_occurrence;
                itm.NumericGrade = _GradeDTO.Numeric_grade;
                itm.Comments = _GradeDTO.Comments;

                _context.Grades.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}
