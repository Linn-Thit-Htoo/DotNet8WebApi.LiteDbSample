using DotNet8WebApi.LiteDbSample.Models;
using DotNet8WebApi.LiteDbSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogV3Controller : ControllerBase
{
    private readonly LiteDbV3Service _liteDbV3Service;
    private readonly string _tableName;

    public BlogV3Controller(LiteDbV3Service liteDbV3Service)
    {
        _liteDbV3Service = liteDbV3Service;
        _tableName = "Blog";
    }

    #region Get Blogs

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var lst = _liteDbV3Service.List<BlogModel>(_tableName);
        return Ok(lst);
    }

    #endregion

    #region Get Blog

    [HttpGet("{id}")]
    public IActionResult GetBlog(string id)
    {
        var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);
        return Ok(item);
    }

    #endregion

    #region Create Blog

    [HttpPost]
    public IActionResult CreateBlog([FromBody] BlogRequestModel requestModel)
    {
        var blog = new BlogModel
        {
            BlogId = Guid.NewGuid().ToString(),
            BlogTitle = requestModel.BlogTitle,
            BlogAuthor = requestModel.BlogAuthor,
            BlogContent = requestModel.BlogContent
        };
        _liteDbV3Service.Add<BlogModel>(blog, _tableName);

        return Ok(blog);
    }

    #endregion

    #region Put

    #endregion
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] BlogRequestModel requestModel)
    {
        var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);

        if (item is null)
            return NotFound("No data found.");

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        var result = _liteDbV3Service.Update<BlogModel>(item, _tableName);

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(string id, [FromBody] BlogRequestModel requestModel)
    {
        var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);

        if (item is null)
            return NotFound("No data found.");

        if (!string.IsNullOrEmpty(requestModel.BlogTitle))
        {
            item.BlogTitle = requestModel.BlogTitle;
        }

        if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            item.BlogAuthor = requestModel.BlogAuthor;
        }

        if (!string.IsNullOrEmpty(requestModel.BlogContent))
        {
            item.BlogContent = requestModel.BlogContent;
        }

        var result = _liteDbV3Service.Update<BlogModel>(item, _tableName);

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(string id)
    {
        var item = _liteDbV3Service.GetById<BlogModel>(x => x.BlogId == id, _tableName);
        if (item is null)
            return NotFound("No data found.");

        var result = _liteDbV3Service.Delete<BlogModel>(item.Id!, _tableName);

        return result ? StatusCode(202, "Deleting Successful.") : BadRequest();
    }
}