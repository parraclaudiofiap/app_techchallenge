using System.Linq.Expressions;
using Gateway;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoRepository.Context;

namespace MongoRepository.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
    }

   protected IList<T> GetList(IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> predicate)
    {
        return mongoCollection
            .AsQueryable<T>()
            .Where(predicate.Compile())
            .ToList();
    }

    protected async Task<T> GetOne(IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(mongoCollection
            .AsQueryable<T>()
            .First(predicate.Compile()));
    }

    protected async Task<bool> InsertOne(IMongoCollection<T> mongoCollection, T obj)
    {
        try
        {
            await mongoCollection.InsertOneAsync(obj);
            return true;
        }
           catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

     protected async Task<bool> Update(IMongoCollection<T> mongoCollection, T obj,  Expression<Func<T, bool>> predicate)
    {
        try
        {
            await mongoCollection.FindOneAndReplaceAsync(predicate, obj);
            return true;
        }
           catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected async Task<bool> Upsert(IMongoCollection<T> mongoCollection, T obj,  Expression<Func<T, bool>> predicate)
    {
        try
        {
            var upsert = new ReplaceOptions() { IsUpsert = true};
            await mongoCollection.ReplaceOneAsync(predicate, obj, upsert);
            return true;
        }
           catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected async Task<bool> Delete(IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> predicate)
    {
        try
        {
            await mongoCollection.FindOneAndDeleteAsync(predicate);
            return true;
        }
           catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}