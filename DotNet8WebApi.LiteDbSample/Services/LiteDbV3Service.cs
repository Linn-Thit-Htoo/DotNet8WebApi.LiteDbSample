using LiteDB;
using System.Linq.Expressions;

namespace DotNet8WebApi.LiteDbSample.Services;

public class LiteDbV3Service
{
    private readonly LiteDatabase _liteDatabase;

    public LiteDbV3Service(LiteDatabase liteDatabase)
    {
        _liteDatabase = liteDatabase;
    }

    #region List

    public List<T> List<T>(string tableOrClassName)
    {
        tableOrClassName ??= typeof(T).Name;
        ILiteCollection<T> lst = tableOrClassName is not null
            ? _liteDatabase.GetCollection<T>(tableOrClassName)
            : _liteDatabase.GetCollection<T>();

        List<T> _list = lst.FindAll().ToList();
        return _list;
    }

    #endregion

    #region Get By Id

    public T GetById<T>(Expression<Func<T, bool>> condition, string tableOrClassName)
    {
        tableOrClassName ??= typeof(T).Name;
        ILiteCollection<T> lst = tableOrClassName is not null
            ? _liteDatabase.GetCollection<T>(tableOrClassName)
            : _liteDatabase.GetCollection<T>();
        var item = lst.Find(condition).FirstOrDefault();

        return item!;
    }

    #endregion

    #region Add

    public BsonValue Add<T>(T requestModel, string tableOrClassName)
    {
        tableOrClassName ??= typeof(T).Name;
        return _liteDatabase.GetCollection<T>(tableOrClassName).Insert(requestModel);
    }

    #endregion

    #region Update

    public bool Update<T>(T requestModel, string tableOrClassName)
    {
        tableOrClassName ??= typeof(T).Name;
        return _liteDatabase.GetCollection<T>(tableOrClassName).Update(requestModel);
    }

    #endregion

    #region Delete

    #endregion
    public bool Delete<T>(ObjectId Id, string tableOrClassName)
    {
        tableOrClassName ??= typeof(T).Name;
        return _liteDatabase.GetCollection<T>(tableOrClassName).Delete(new BsonValue(Id));
    }
}