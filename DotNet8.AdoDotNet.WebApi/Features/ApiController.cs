using DotNet8.AdoDotNet.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.AdoDotNet.WebApi.Features;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService =
        new AdoDotNetService(SqlConnectionString.sqlConnectionStringBuilder.ConnectionString);

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put()
    {
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> Patch()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}