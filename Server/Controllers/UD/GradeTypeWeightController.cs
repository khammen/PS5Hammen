﻿using Microsoft.AspNetCore.Mvc;
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
    public class GradeTypeWeightController : BaseController, GenericRestController<GradeTypeWeightDTO>
    {
        public GradeTypeWeightController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{SectionID}/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(int SchoolID, int SectionID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypeWeights.Remove(itm);
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
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.GradeTypeWeights.Select(sp => new GradeTypeWeightDTO
                {
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    DropLowest = sp.DropLowest
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
        [Route("Get/{SchoolID}/{SectionID}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeWeightDTO? result = await _context
                    .GradeTypeWeights
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                     .Select(sp => new GradeTypeWeightDTO
                     {
                         SchoolId = sp.SchoolId,
                         SectionId = sp.SectionId,
                         GradeTypeCode = sp.GradeTypeCode,
                         NumberPerSection = sp.NumberPerSection,
                         PercentOfFinalGrade = sp.PercentOfFinalGrade,
                         DropLowest = sp.DropLowest
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
        public async Task<IActionResult> Post([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();
                if (itm == null)
                {
                    GradeTypeWeight e = new GradeTypeWeight
                    {
                        SchoolId = _GradeTypeWeightDTO.SchoolId,
                        SectionId = _GradeTypeWeightDTO.SectionId,
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        NumberPerSection = (byte)_GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = (byte)_GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest
                    };
                    _context.GradeTypeWeights.Add(e);
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
        public async Task<IActionResult> Put([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();

                itm.SchoolId = _GradeTypeWeightDTO.SchoolId;
                itm.SectionId = _GradeTypeWeightDTO.SectionId;
                itm.GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode;
                itm.NumberPerSection = (byte)_GradeTypeWeightDTO.NumberPerSection;
                itm.PercentOfFinalGrade = (byte)_GradeTypeWeightDTO.PercentOfFinalGrade;
                itm.DropLowest = _GradeTypeWeightDTO.DropLowest;

                _context.GradeTypeWeights.Update(itm);
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
