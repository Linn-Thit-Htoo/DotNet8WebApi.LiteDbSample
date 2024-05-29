using LiteDB;

namespace DotNet8WebApi.LiteDbSample.Services
{
    public class LiteDbV3Service
    {
        private readonly LiteDatabase _liteDatabase;

        public LiteDbV3Service(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public List<T> List<T>(string tableOrClassName)
        {
            tableOrClassName ??= typeof(T).Name;
            ILiteCollection<T> lst = tableOrClassName is not null 
                ? _liteDatabase.GetCollection<T>(tableOrClassName)
                : _liteDatabase.GetCollection<T>();

            List<T> _list = lst.FindAll().ToList();
            return _list;
        }
    }
}
