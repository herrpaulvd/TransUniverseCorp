using BL.Repos;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReposImpl
{
    internal abstract class ExtendedRepoImpl<TBLEntity, TDALEntity>
        : UniversalRepoImpl<TBLEntity, TDALEntity>
        , IExtendedRepo<TBLEntity>
        where TBLEntity : class, INamedBLEntity, new()
        where TDALEntity : class, INamedDALEntity, new()
    {
        protected ExtendedRepoImpl(DbContext context, Func<DbContext, DbSet<TDALEntity>> getEntities)
            : base(context, getEntities) { }

        public TBLEntity? FindByName(string name)
        {
            var dalEntity = entities.FirstOrDefault(e => e.Name == name);
            if(dalEntity is not null) context.Entry(dalEntity).State = EntityState.Detached;
            return GetBLEntity(dalEntity);
        }
    }
}
