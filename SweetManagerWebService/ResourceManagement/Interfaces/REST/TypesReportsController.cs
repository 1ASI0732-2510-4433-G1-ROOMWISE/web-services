using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.TypeReport;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Transform.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
public class TypesReportsController(ITypeReportQueryService typeReportQueryService) :
    ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> AllTypesReports()
    {
        var typesReports = await typeReportQueryService
            .Handle(new GetAllTypesReportsQuery());

        var typesReportsResource = typesReports.Select
        (TypeReportResourceFromEntityAssembler
            .ToResourceFromEntity);

        return Ok(typesReportsResource);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> TypeReportById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid Id");
        }

        try
        {
            var typeReport = await typeReportQueryService.Handle(new GetTypeReportByIdQuery(id));

            if (typeReport is null)
            {
                throw new Exception("TypeReport not found");
            }

            var typeReportResource = TypeReportResourceFromEntityAssembler.ToResourceFromEntity(typeReport);

            return Ok(typeReportResource);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

