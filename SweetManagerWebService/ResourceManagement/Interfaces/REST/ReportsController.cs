using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.Report;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.Report;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Transform.Report;

namespace SweetManagerWebService.ResourceManagement.Interfaces.REST
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReportsController(IReportCommandService reportCommandService, IReportQueryService reportQueryService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportResource resource)
        {
            var result = await reportCommandService
                .Handle(CreateReportCommandFromResourceAssembler
                    .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest("Error creating report.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AllReports([FromQuery] int hotelId)
        {
            try
            {
                // Pass HotelId into the query
                var result = await reportQueryService.Handle(new GetAllReportsQuery(hotelId));

                // Map the result to a collection of supply resources
                var reportResource = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(reportResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReportById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }

            try
            {
                var report = await reportQueryService.Handle(new GetReportByIdQuery(id));

                if (report is null)
                {
                    throw new Exception("Report not found");
                }

                var reportResource = ReportResourceFromEntityAssembler.ToResourceFromEntity(report);
                return Ok(reportResource);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}