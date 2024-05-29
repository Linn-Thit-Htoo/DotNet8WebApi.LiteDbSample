using DotNet8WebApi.LiteDbSample.Models;
using DotNet8WebApi.LiteDbSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.LiteDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogV3Controller : ControllerBase
    {
        private readonly LiteDbV3Service _liteDbV3Service;

        public BlogV3Controller(LiteDbV3Service liteDbV3Service)
        {
            _liteDbV3Service = liteDbV3Service;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _liteDbV3Service.List<BlogModel>("Blog");
            return Ok(lst);
        }
    }
}
