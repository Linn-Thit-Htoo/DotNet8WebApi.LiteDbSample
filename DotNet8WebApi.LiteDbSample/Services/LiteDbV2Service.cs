using DotNet8WebApi.LiteDbSample.Models;
using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Services
{
    public class LiteDbV2Service
    {
        private readonly LiteDatabase _liteDatabase;

        public LiteDbV2Service(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        //public ILiteCollection<BlogModel> Blogs()
        //{
        //    return _liteDatabase.GetCollection<BlogModel>("Blog");
        //}

        public ILiteCollection<BlogModel> Blogs => _liteDatabase.GetCollection<BlogModel>("Blog");

        public void Dispose() => _liteDatabase.Dispose();
    }
}