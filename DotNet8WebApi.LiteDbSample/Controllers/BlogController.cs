using DotNet8WebApi.LiteDbSample.Models;
using LiteDB;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly string _filePath;
    private readonly string _folderPath;

    public BlogController()
    {
        _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
        Directory.CreateDirectory(_folderPath);
        _filePath = Path.Combine(_folderPath, "Blog.db");
    }

    #region Get Blogs

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var lst = collection.FindAll().ToList();
        db.Dispose();

        return Ok(lst);
    }

    #endregion

    #region Get Blog

    [HttpGet("{id}")]
    public IActionResult GetBlog(string id)
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
        db.Dispose();

        return Ok(item);
    }

    #endregion

    #region Create Blog

    [HttpPost]
    public IActionResult CreateBlog([FromBody] BlogRequestModel requestModel)
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var blog = new BlogModel
        {
            BlogId = Guid.NewGuid().ToString(),
            BlogTitle = requestModel.BlogTitle,
            BlogAuthor = requestModel.BlogAuthor,
            BlogContent = requestModel.BlogContent
        };
        collection.Insert(blog);
        db.Dispose();

        return Ok(blog);
    }

    #endregion

    #region Put

    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] BlogRequestModel requestModel)
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var item = collection.Find(x => x.BlogId == id).FirstOrDefault();

        if (item is null)
            return NotFound("No data found.");

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        var result = collection.Update(item);
        db.Dispose();

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    #endregion

    #region Patch

    [HttpPatch("{id}")]
    public IActionResult Patch(string id, [FromBody] BlogRequestModel requestModel)
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var item = collection.Find(x => x.BlogId == id).FirstOrDefault();

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

        var result = collection.Update(item);
        db.Dispose();

        return result ? StatusCode(202, "Updating Successful.") : BadRequest();
    }

    #endregion

    #region MyRegion

    #endregion
    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(string id)
    {
        var db = new LiteDatabase(_filePath);
        var collection = db.GetCollection<BlogModel>("Blog");
        var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
        if (item is null)
            return NotFound("No data found.");

        var result = collection.Delete(item.Id);
        db.Dispose();

        return result ? StatusCode(202, "Deleting Successful.") : BadRequest();
    }
}