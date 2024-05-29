using DotNet8WebApi.LiteDbSample.Models;
using DotNet8WebApi.LiteDbSample.Services;
using LiteDB;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogV2Controller : ControllerBase
{
    private readonly LiteDbV2Service _liteDbV2Service;

    public BlogV2Controller(LiteDbV2Service liteDbV2Service)
    {
        _liteDbV2Service = liteDbV2Service;
    }

    #region Get Blogs

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var lst = _liteDbV2Service.Blogs.FindAll().ToList();
        return Ok(lst);
    }

    #endregion

    #region Get Blog

    [HttpGet("{id}")]
    public IActionResult GetBlog(string id)
    {
        var item = _liteDbV2Service.Blogs.Find(x => x.BlogId == id).FirstOrDefault();
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
        _liteDbV2Service.Blogs.Insert(blog);

        return Ok(blog);
    }

    #endregion

    #region Put

    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] BlogRequestModel requestModel)
    {
        var item = _liteDbV2Service.Blogs.Find(x => x.BlogId == id).FirstOrDefault();

        if (item is null)
            return NotFound("No data found.");

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        var result = _liteDbV2Service.Blogs.Update(item);

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    #endregion

    #region Patch

    [HttpPatch("{id}")]
    public IActionResult Patch(string id, [FromBody] BlogRequestModel requestModel)
    {
        var item = _liteDbV2Service.Blogs.Find(x => x.BlogId == id).FirstOrDefault();

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

        var result = _liteDbV2Service.Blogs.Update(item);

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    #endregion

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(string id)
    {
        var item = _liteDbV2Service.Blogs.Find(x => x.BlogId == id).FirstOrDefault();
        if (item is null)
            return NotFound("No data found.");

        var result = _liteDbV2Service.Blogs.Delete(item.Id);

        return result ? StatusCode(202, "Deleting Successful.") : BadRequest();
    }
}