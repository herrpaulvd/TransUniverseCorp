using BL.Repos;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReposImpl
{
    internal abstract class UniversalRepoImpl<TBLEntity, TDALEntity>
        : BaseUniversalRepoImpl, IUniversalRepo<TBLEntity>
        where TBLEntity : class, IBLEntity, new()
        where TDALEntity : class, IDALEntity, new()
    {
        protected DbContext context;
        protected DbSet<TDALEntity> entities;

        protected UniversalRepoImpl(DbContext context, Func<DbContext, DbSet<TDALEntity>> getEntities)
        {
            this.context = context;
            entities = getEntities(context);
        }

        protected static TBLEntity? GetBLEntity(TDALEntity? entity) => Convert<TDALEntity, TBLEntity>(entity);
        protected static TDALEntity? GetDalEntity(TBLEntity? entity) => Convert<TBLEntity, TDALEntity>(entity);

        public void Delete(TBLEntity entity)
        {
            if (!entity.CheckConsistencyOnDelete()) throw new InconsistencyException();
            entities.Remove(GetDalEntity(entity)!);
            context.SaveChanges();
        }

        public TBLEntity? Get(int id)
        {
            var retrieved = entities.FirstOrDefault(e => e.Id == id);
            if (retrieved is not null) context.Entry(retrieved).State = EntityState.Detached;
            return GetBLEntity(retrieved);
        }

        public IList<TBLEntity?> GetAll()
        {
            return entities.AsEnumerable().Select(e =>
            {
                context.Entry(e).State = EntityState.Detached;
                return GetBLEntity(e);
            }).ToArray();
        }

        public void Update(TBLEntity entity)
        {
            if (!entity.CheckConsistency()) throw new InconsistencyException();
            var dalEntity = GetDalEntity(entity)!;
            entities.Update(dalEntity);
            context.SaveChanges();
            context.Entry(dalEntity).State = EntityState.Detached;
        }

        public int Add(TBLEntity entity)
        {
            if (!entity.CheckConsistency()) throw new InconsistencyException();
            var dalEntity = GetDalEntity(entity)!;
            entities.Add(dalEntity);
            context.SaveChanges();
            context.Entry(dalEntity).State = EntityState.Detached;
            return dalEntity.Id;
        }
    }
}
