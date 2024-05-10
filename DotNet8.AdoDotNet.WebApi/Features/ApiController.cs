

namespace DotNet8.AdoDotNet.WebApi.Features;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly IApplication _application;

    public ApiController(IApplication application) => _application = application;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _application.GetBlogs();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var item = await _application.GetBlogById(id);
            if (item is null) return NotFound("No Data Found.");
            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(BlogDataModel reqModel)
    {
        try
        {
            var item = await _application.CreateBlog(reqModel);
            var message = item > 0 ? "Saving Successful" : "Saving Failed.";
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, BlogDataModel reqModel)
    {
        try
        {
            var item = await _application.PutBlog(id, reqModel);
            var message = item > 0 ? "Saving Successful" : "Saving Failed.";
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, BlogDataModel reqModel)
    {
        try
        {
            var item = await _application.PatchBlog(id, reqModel);
            var message = item > 0 ? "Saving Successful" : "Saving Failed.";
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var item = await _application.DeleteBlog(id);
            var message = item > 0 ? "Deleting Successful" : "Deleting Failed.";
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}